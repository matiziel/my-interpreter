using System;
using System.Collections.Generic;
using MyInterpreter.Exceptions.ParserExceptions;
using MyInterpreter.Lexer;
using MyInterpreter.Lexer.Tokens;
using MyInterpreter.Parser.Ast;
using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Statements;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser {
    public class Parser {
        private readonly IScanner _scanner;
        private readonly IDictionary<string, Function> _functions;
        private readonly ICollection<Definition> _definitions;
        private readonly Dictionary<TokenType, Func<IOperator>> _operatorsMapper;
        public Parser(IScanner scanner) {
            _scanner = scanner;
            _functions = new Dictionary<string, Function>();
            _definitions = new List<Definition>();
            _operatorsMapper = Mapper.GetOperators();
        }
        public Program Parse() {
            _scanner.Next();
            while (TryParseDefinitionOrFunction())
                ;
            return new Program(_functions, _definitions);
        }
        private bool TryParseDefinitionOrFunction() {
            if (_scanner.CurrentToken.Type == TokenType.EOT)
                return false;

            TypeValue? type = TryParseType() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
            string name = TryParseIdentifier() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            Definition def;
            Function fun;
            if ((fun = TryParseFunction(type.Value, name)) != null)
                _functions.Add(name, fun);
            else if ((def = TryParseDefinition(type.Value, name)) != null) {
                if (_scanner.CurrentToken.Type == TokenType.SEMICOLON) {
                    _definitions.Add(def);
                    _scanner.Next();
                }
                else
                    throw new UnexpectedToken(_scanner.CurrentToken.Position);
            }
            else
                throw new UnexpectedToken(_scanner.CurrentToken.Position);

            return true;
        }
        private TypeValue? TryParseType() {
            TypeValue? typename = TryToGetType(_scanner.CurrentToken);
            if (typename is null)
                return null;
            _scanner.Next();
            return typename;
        }
        private TypeValue? TryToGetType(Token token) {
            if (token.Type == TokenType.INT)
                return TypeValue.Int;
            else if (token.Type == TokenType.STRING)
                return TypeValue.String;
            else if (token.Type == TokenType.VOID)
                return TypeValue.Void;
            else if (token.Type == TokenType.MATRIX)
                return TypeValue.Matrix;
            else
                return null;
        }
        private string TryParseIdentifier() {
            string name;
            if (_scanner.CurrentToken.Type != TokenType.IDENTIFIER)
                return null;
            name = _scanner.CurrentToken.ToString();
            _scanner.Next();
            return name;
        }
        private Definition TryParseDefinition(TypeValue type, string name) {
            Variable variable = TryParseVariable(type, name);
            if (_scanner.CurrentToken.Type == TokenType.ASSIGN) {
                _scanner.Next();
                Expression expr = TryParseExpression() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
                return new Definition(variable, expr);
            }
            else
                return new Definition(variable);

        }
        private Variable TryParseVariable(TypeValue type, string name) {
            if (_scanner.CurrentToken.Type != TokenType.BRACKET_OPEN)
                return new Variable(type, name);

            if (type != TypeValue.Matrix)
                throw new UnexpectedToken(_scanner.CurrentToken.Position);
            _scanner.Next();

            Expression first = TryParseExpression() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            CheckTokenTypeAndGoNext(TokenType.COMMA);

            Expression second = TryParseExpression() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            CheckTokenTypeAndGoNext(TokenType.BRACKET_CLOSE);

            return new Variable(type, name, first, second);

        }
        private void CheckTokenTypeAndGoNext(TokenType expected) {
            if (_scanner.CurrentToken.Type != expected)
                throw new UnexpectedToken(_scanner.CurrentToken.Position);
            _scanner.Next();
        }
        private Function TryParseFunction(TypeValue type, string name) {
            if (_scanner.CurrentToken.Type != TokenType.PAREN_OPEN)
                return null;
            _scanner.Next();

            IEnumerable<Parameter> parameters = TryParseParameterList() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            CheckTokenTypeAndGoNext(TokenType.PAREN_CLOSE);

            BlockStatement statement = TryParseBlockStatement() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            return new Function(type, name, parameters, statement);
        }
        private List<Parameter> TryParseParameterList() {
            var parameters = new List<Parameter>();

            Parameter param = TryParseParameter();
            if (param is null)
                return parameters;
            parameters.Add(param);

            while (_scanner.CurrentToken.Type == TokenType.COMMA) {
                _scanner.Next();
                param = TryParseParameter() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
                parameters.Add(param);
            }
            return parameters;
        }
        private Parameter TryParseParameter() {
            TypeValue? type = TryParseType();
            if (type is null)
                return null;
            string name = TryParseIdentifier() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
            return new Parameter(type.Value, name);
        }
        private Statement TryParseStatement() {
            Statement statement;
            if ((statement = TryParseBlockStatement()) != null)
                return statement;
            else if ((statement = TryParseIfStatement()) != null)
                return statement;
            else if ((statement = TryParseWhileStatement()) != null)
                return statement;
            else if ((statement = TryParseForStatement()) != null)
                return statement;
            else if ((statement = TryParseReturnStatement()) != null) {
                CheckTokenTypeAndGoNext(TokenType.SEMICOLON);
                return statement;
            }
            else if ((statement = TryParseAssignmentOrFunctionCall()) != null) {
                CheckTokenTypeAndGoNext(TokenType.SEMICOLON);
                return statement;
            }
            else if ((statement = TryParseDefinition()) != null) {
                CheckTokenTypeAndGoNext(TokenType.SEMICOLON);
                return statement;
            }
            return null;
        }
        private BlockStatement TryParseBlockStatement() {
            if (_scanner.CurrentToken.Type != TokenType.BRACE_OPEN)
                return null;

            _scanner.Next();
            var statements = new List<Statement>();
            Statement stat;
            while ((stat = TryParseStatement()) != null)
                statements.Add(stat);

            CheckTokenTypeAndGoNext(TokenType.BRACE_CLOSE);

            return new BlockStatement(statements);
        }
        private IfStatement TryParseIfStatement() {
            if (_scanner.CurrentToken.Type != TokenType.IF)
                return null;
            _scanner.Next();

            CheckTokenTypeAndGoNext(TokenType.PAREN_OPEN);

            Conditional conditional = TryParseConditional() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            CheckTokenTypeAndGoNext(TokenType.PAREN_CLOSE);

            Statement statementIf = TryParseStatement() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            if (_scanner.CurrentToken.Type != TokenType.ELSE)
                return new IfStatement(conditional, statementIf);
            _scanner.Next();

            Statement statementElse = TryParseStatement() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            return new IfStatement(conditional, statementIf, statementElse);
        }
        private WhileStatement TryParseWhileStatement() {
            if (_scanner.CurrentToken.Type != TokenType.WHILE)
                return null;
            _scanner.Next();

            CheckTokenTypeAndGoNext(TokenType.PAREN_OPEN);

            Conditional conditional = TryParseConditional() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            CheckTokenTypeAndGoNext(TokenType.PAREN_CLOSE);

            Statement statement = TryParseStatement() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            return new WhileStatement(conditional, statement);
        }
        private ForStatement TryParseForStatement() {
            if (_scanner.CurrentToken.Type != TokenType.FOR)
                return null;
            _scanner.Next();

            CheckTokenTypeAndGoNext(TokenType.PAREN_OPEN);

            Assignment assignmentFirst = null;
            string name = TryParseIdentifier();
            if (!(name is null)) {
                assignmentFirst = TryParseAssignment(name) ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
            }
            CheckTokenTypeAndGoNext(TokenType.SEMICOLON);

            Conditional conditional = TryParseConditional();

            CheckTokenTypeAndGoNext(TokenType.SEMICOLON);

            Assignment assignmentSecond = null;
            name = TryParseIdentifier();
            if (!(name is null)) {
                assignmentSecond = TryParseAssignment(name) ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
            }
            CheckTokenTypeAndGoNext(TokenType.PAREN_CLOSE);

            Statement statement = TryParseStatement() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            return new ForStatement(statement, conditional, assignmentFirst, assignmentSecond);
        }
        private ReturnStatement TryParseReturnStatement() {
            if (_scanner.CurrentToken.Type != TokenType.RETURN)
                return null;
            _scanner.Next();

            Expression expression = TryParseExpression() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            return new ReturnStatement(expression);
        }
        private Statement TryParseAssignmentOrFunctionCall() {
            string name = TryParseIdentifier();
            if (name is null)
                return null;

            Statement statement;
            if ((statement = TryParseFunctionCall(name)) != null)
                return statement;
            else if ((statement = TryParseAssignment(name)) != null)
                return statement;
            else
                throw new UnexpectedToken(_scanner.CurrentToken.Position);
        }
        private FunctionCall TryParseFunctionCall(string name) {
            if (_scanner.CurrentToken.Type != TokenType.PAREN_OPEN)
                return null;
            _scanner.Next();

            IEnumerable<Expression> arguments = TryParseArgumentList() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            CheckTokenTypeAndGoNext(TokenType.PAREN_CLOSE);
            return new FunctionCall(name, arguments);
        }

        private IEnumerable<Expression> TryParseArgumentList() {
            var arguments = new List<Expression>();

            Expression expr = TryParseExpression();
            if (expr is null)
                return arguments;
            arguments.Add(expr);

            while (_scanner.CurrentToken.Type == TokenType.COMMA) {
                _scanner.Next();
                expr = TryParseExpression() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
                arguments.Add(expr);
            }
            return arguments;
        }
        private Assignment TryParseAssignment(string name) {
            DerefVariable derefVar = TryParseDerefVariable(name);
            if (derefVar is null)
                return null;

            IOperator assignmentOperator = TryParseOperator() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
            _scanner.Next();

            Expression expression = TryParseExpression() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            return new Assignment(name, assignmentOperator as AssignmentOperator, expression);
        }
        private Definition TryParseDefinition() {
            TypeValue? type = TryParseType();
            if (type is null)
                return null;

            string name = TryParseIdentifier() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
            Definition def = TryParseDefinition(type.Value, name) ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            return def;
        }
        private Conditional TryParseConditional() {
            Conditional left = TryParseAndConditional();
            if (left is null)
                return null;

            while (_scanner.CurrentToken.Type == TokenType.OR) {
                _scanner.Next();
                Conditional right = TryParseAndConditional() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
                left = new OrConditional(left, right);
            }
            return left;
        }
        private Conditional TryParseAndConditional() {
            Conditional left = TryParseLogical();
            if (left is null)
                return null;

            while (_scanner.CurrentToken.Type == TokenType.AND) {
                _scanner.Next();
                Conditional right = TryParseLogical() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
                left = new AndConditional(left, right);
            }
            return left;
        }
        private Logical TryParseLogical() {
            Logical logical;
            if ((logical = TryParseParenConditional()) != null)
                return logical;
            else if ((logical = TryParseSimpleConditional()) != null)
                return logical;
            else return null;
        }
        private SimpleConditional TryParseSimpleConditional() {
            Expression left = TryParseExpression();
            if (left is null)
                return null;

            EqualityOperator equalityOperator = TryParseEqualityOperator() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
            Expression right = TryParseExpression() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            return new SimpleConditional(left, right, equalityOperator);
        }
        private ParenConditional TryParseParenConditional() {
            bool isNegated = false;
            if (_scanner.CurrentToken.Type == TokenType.NOT) {
                isNegated = true;
                _scanner.Next();
            }

            if (_scanner.CurrentToken.Type != TokenType.PAREN_OPEN && !isNegated)
                return null;

            if (_scanner.CurrentToken.Type != TokenType.PAREN_OPEN && isNegated)
                throw new UnexpectedToken(_scanner.CurrentToken.Position);

            _scanner.Next();

            Conditional conditional = TryParseConditional() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            CheckTokenTypeAndGoNext(TokenType.PAREN_CLOSE);
            return new ParenConditional(conditional, isNegated);
        }
        private EqualityOperator TryParseEqualityOperator() {
            IOperator equalityOperator = TryParseOperator();
            if (equalityOperator is null)
                return null;
            _scanner.Next();
            if (equalityOperator is EqualityOperator)
                return equalityOperator as EqualityOperator;
            else
                return null;
        }
        private Expression TryParseExpression() {
            Expression left = TryParseMultiplicativeExpression();
            if (left is null)
                return null;

            IOperator additiveOperator;
            while ((additiveOperator = TryParseOperator()) is AdditiveOperator) {
                _scanner.Next();
                Expression right = TryParseMultiplicativeExpression() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
                left = new AdditiveExpression(left, right, additiveOperator as AdditiveOperator);
            }
            return left;
        }
        private Expression TryParseMultiplicativeExpression() {
            Expression left;
            if ((left = TryParseUnary()) == null)
                return null;

            IOperator multiplicativeOperator;
            while ((multiplicativeOperator = TryParseOperator()) is MultiplicativeOperator) {
                _scanner.Next();
                Expression right = TryParseUnary() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);
                left = new MultiplicativeExpression(left, right, multiplicativeOperator as MultiplicativeOperator);
            }
            return left;
        }
        private UnaryExpression TryParseUnary() {
            bool isNegated = _scanner.CurrentToken.Type == TokenType.MINUS;
            if (isNegated)
                _scanner.Next();

            PrimaryExpression expression;
            if ((expression = TryParsePrimaryExpression()) != null)
                return new UnaryExpression(expression, isNegated);
            else if (isNegated)
                throw new UnexpectedToken(_scanner.CurrentToken.Position);
            else
                return null;
        }
        private PrimaryExpression TryParsePrimaryExpression() {
            PrimaryExpression expression;
            if ((expression = TryParseConstantExpression()) != null)
                return expression;
            else if ((expression = TryParseDerefVarOrFunCall()) != null)
                return expression;
            else if ((expression = TryParseParenExpression()) != null)
                return expression;
            else
                return null;
        }
        private ConstantExpression TryParseConstantExpression() {
            Value value = TryParseValue();
            if (value is null)
                return null;

            return new ConstantExpression(value);
        }
        private PrimaryExpression TryParseDerefVarOrFunCall() {
            string name = TryParseIdentifier();
            if (name is null)
                return null;

            PrimaryExpression expression;
            if ((expression = TryParseFunctionCall(name)) != null)
                return expression;
            else if ((expression = TryParseDerefVariable(name)) != null)
                return expression;
            else
                throw new UnexpectedToken(_scanner.CurrentToken.Position);
        }
        private DerefVariable TryParseDerefVariable(string name) {
            if (_scanner.CurrentToken.Type != TokenType.BRACKET_OPEN)
                return new DerefVariable(name);
            _scanner.Next();

            Ast.Range left = TryParseRange() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            CheckTokenTypeAndGoNext(TokenType.COMMA);

            Ast.Range right = TryParseRange() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            CheckTokenTypeAndGoNext(TokenType.BRACKET_CLOSE);
            return new DerefVariable(name, left, right);
        }
        private Ast.Range TryParseRange() {
            Expression first = TryParseExpression();
            if (first is null)
                return null;
            CheckTokenTypeAndGoNext(TokenType.COLON);

            Expression second = TryParseExpression() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            return new Ast.Range(first, second);
        }
        private ParenExpression TryParseParenExpression() {
            if (_scanner.CurrentToken.Type != TokenType.PAREN_OPEN)
                return null;
            _scanner.Next();

            Expression expression = TryParseExpression() ?? throw new UnexpectedToken(_scanner.CurrentToken.Position);

            CheckTokenTypeAndGoNext(TokenType.PAREN_CLOSE);
            return new ParenExpression(expression);
        }
        private Value TryParseValue() {
            Value value;
            if (_scanner.CurrentToken.Type == TokenType.NUMBER)
                value = new Int_t((_scanner.CurrentToken as Number).Value);
            else if (_scanner.CurrentToken.Type == TokenType.TEXT)
                value = new String_t(_scanner.CurrentToken.ToString());
            else
                return null;

            _scanner.Next();
            return value;
        }
        private IOperator TryParseOperator() {
            if (!_operatorsMapper.ContainsKey(_scanner.CurrentToken.Type))
                return null;
            IOperator value = _operatorsMapper[_scanner.CurrentToken.Type]();
            return value;
        }
    }
}