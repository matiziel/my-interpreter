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

namespace MyInterpreter.Parser
{
    public class Parser
    {
        private readonly IScanner _scanner;
        private readonly IDictionary<string, Function> _functions;
        private readonly ICollection<Definition> _definitions;
        private readonly Dictionary<TokenType, Func<IOperator>> _operatorsMapper;
        public Parser(IScanner scanner)
        {
            _scanner = scanner;
            _functions = new Dictionary<string, Function>();
            _definitions = new List<Definition>();
            _operatorsMapper = Mapper.GetOperators();
        }
        public Program Parse()
        {
            _scanner.Next();
            while(TryParseDefinitionOrFunction())
                ;
            return new Program(_functions, _definitions);
        }
        private bool TryParseDefinitionOrFunction()
        {
            if(_scanner.CurrentToken.Type == TokenType.EOT)
                return false;

            string type = TryParseType() ?? throw new UnexpectedToken();
            string name = TryParseIdentifier() ?? throw new UnexpectedToken();

            Definition def;
            Function fun;
            if((fun = TryParseFunction(type, name)) != null)
                _functions.Add(name, fun);
            else if((def = TryParseDefinition(type, name)) != null)
            {
                if(_scanner.CurrentToken.Type == TokenType.SEMICOLON)
                {
                    _definitions.Add(def);
                    _scanner.Next();
                }
                else
                    throw new UnexpectedToken();
            }
            else 
                throw new UnexpectedToken();

            return true;
        }
        private string TryParseType()
        {
            string typename = TryToGetType(_scanner.CurrentToken);
            if(typename is null)
                return null;
            _scanner.Next();
            return typename;
        }
        private string TryToGetType(Token token)
        {
            if(token.Type != TokenType.INT && token.Type != TokenType.STRING
                && token.Type != TokenType.VOID && token.Type != TokenType.MATRIX)
                return null;
            return token.ToString();
        }
        private string TryParseIdentifier()
        {
            string name;
            if(_scanner.CurrentToken.Type != TokenType.IDENTIFIER)
                return null;
            name = _scanner.CurrentToken.ToString();
            _scanner.Next();
            return name;
        }
        private Definition TryParseDefinition(string type, string name)
        {
            Variable variable = TryParseVariable(type, name);
            if(_scanner.CurrentToken.Type == TokenType.ASSIGN)
            {
                _scanner.Next();
                Expression expr = TryParseExpression() ?? throw new UnexpectedToken();
                return new Definition(variable, expr);
            }
            else
                return new Definition(variable);

        }
        private Variable TryParseVariable(string type, string name)
        {
            if(_scanner.CurrentToken.Type != TokenType.BRACKET_OPEN)
                return new Variable(type, name);

            if(type != "matrix")
                throw new UnexpectedToken();
            _scanner.Next();

            Expression first = TryParseExpression() ?? throw new UnexpectedToken();

            if(_scanner.CurrentToken.Type != TokenType.COMMA)
                throw new UnexpectedToken();
            _scanner.Next();

            Expression second = TryParseExpression() ?? throw new UnexpectedToken();

            return new Variable(type, name, first, second);

        }
        private Function TryParseFunction(string type, string name)
        {
            if(_scanner.CurrentToken.Type != TokenType.PAREN_OPEN)
                return null;
            _scanner.Next();

            IEnumerable<Parameter> parameters = TryParseParameterList() ?? throw new UnexpectedToken();;

            if(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
                throw new UnexpectedToken();
            _scanner.Next();

            BlockStatement statement = TryParseBlockStatement() ?? throw new UnexpectedToken();

            return new Function(type, name, parameters, statement);
        }
        private List<Parameter> TryParseParameterList()
        {
            var parameters = new List<Parameter>();

            Parameter param = TryParseParameter();
            if(param is null)
                return parameters;
            parameters.Add(param);

            while(_scanner.CurrentToken.Type == TokenType.COMMA) 
            {
                _scanner.Next();
                param = TryParseParameter() ?? throw new UnexpectedToken();
                parameters.Add(param);
            }
            return parameters;
        }
        private Parameter TryParseParameter()
        {
            string type = TryParseType();
            if(type is null)
                return null;
            string name = TryParseIdentifier() ?? throw new UnexpectedToken();
            return new Parameter(type, name);
        }
        private Statement TryParseStatement()
        {
            Statement statement;
            if((statement = TryParseBlockStatement()) != null)
                return statement;
            else if((statement = TryParseIfStatement()) != null)
                return statement;
            else if((statement = TryParseWhileStatement()) != null)
                return statement;
            else if((statement = TryParseForStatement()) != null)
                return statement;
            else if((statement = TryParseReturnStatement()) != null)
            {
                if(_scanner.CurrentToken.Type != TokenType.SEMICOLON)
                    throw new UnexpectedToken();
                _scanner.Next();
                return statement;
            }
            else if((statement = TryParseAssignmentOrFunctionCall()) != null)
            {
                if(_scanner.CurrentToken.Type != TokenType.SEMICOLON)
                    throw new UnexpectedToken();
                _scanner.Next();
                return statement;
            }
            else if((statement = TryParseDefinition()) != null)
            {
                if(_scanner.CurrentToken.Type != TokenType.SEMICOLON)
                    throw new UnexpectedToken();
                _scanner.Next();
                return statement;
            }
            return null;
        }
        private BlockStatement TryParseBlockStatement()
        {
            if(_scanner.CurrentToken.Type != TokenType.BRACE_OPEN)
                return null;
            
            _scanner.Next();
            var statements = new List<Statement>();
            Statement stat;
            while((stat = TryParseStatement()) != null)
                statements.Add(stat);
            
            if(_scanner.CurrentToken.Type != TokenType.BRACE_CLOSE)
                throw new UnexpectedToken();

            _scanner.Next();
            return new BlockStatement(statements);
        }
        private IfStatement TryParseIfStatement()
        {
            if(_scanner.CurrentToken.Type != TokenType.IF)
                return null;
            _scanner.Next();

            if(_scanner.CurrentToken.Type != TokenType.PAREN_OPEN)
                throw new UnexpectedToken();
            _scanner.Next();

            Conditional conditional = TryParseConditional() ?? throw new UnexpectedToken();

            if(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
                throw new UnexpectedToken();
            _scanner.Next();

            Statement statementIf = TryParseStatement() ?? throw new UnexpectedToken();

             if(_scanner.CurrentToken.Type != TokenType.ELSE)
                return new IfStatement(conditional, statementIf);
            _scanner.Next();

            Statement statementElse = TryParseStatement() ?? throw new UnexpectedToken();

            return new IfStatement(conditional, statementIf, statementElse);
        }
        private WhileStatement TryParseWhileStatement()
        {
            if(_scanner.CurrentToken.Type != TokenType.WHILE)
                return null;
            _scanner.Next();

            if(_scanner.CurrentToken.Type != TokenType.PAREN_OPEN)
                throw new UnexpectedToken();
            _scanner.Next();

            Conditional conditional = TryParseConditional() ?? throw new UnexpectedToken();

            if(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
                throw new UnexpectedToken();
            _scanner.Next();

            Statement statement = TryParseStatement() ?? throw new UnexpectedToken();

            return new WhileStatement(conditional, statement);   
        }
        private ForStatement TryParseForStatement()
        {
            if(_scanner.CurrentToken.Type != TokenType.FOR)
                return null;
            _scanner.Next();

            if(_scanner.CurrentToken.Type != TokenType.PAREN_OPEN)
                throw new UnexpectedToken();
            _scanner.Next();
            
            Assignment assignmentFirst = null;
            string name = TryParseIdentifier();
            if(!(name is null))
            {
                assignmentFirst = TryParseAssignment(name) ?? throw new UnexpectedToken();
            }

            if(_scanner.CurrentToken.Type != TokenType.SEMICOLON)
                throw new UnexpectedToken();
            _scanner.Next();

            Conditional conditional = TryParseConditional();

            if(_scanner.CurrentToken.Type != TokenType.SEMICOLON)
                throw new UnexpectedToken();
            _scanner.Next();

            Assignment assignmentSecond = null;
            name = TryParseIdentifier();
            if(!(name is null))
            {
                assignmentFirst = TryParseAssignment(name) ?? throw new UnexpectedToken();
            }
            if(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
                throw new UnexpectedToken();
            _scanner.Next();

            Statement statement = TryParseStatement() ?? throw new UnexpectedToken();

            return new ForStatement(statement, conditional, assignmentFirst, assignmentSecond);
        }
        private ReturnStatement TryParseReturnStatement()
        {
            if(_scanner.CurrentToken.Type != TokenType.RETURN)
                return null;
            _scanner.Next();

            Expression expression = TryParseExpression() ?? throw new UnexpectedToken();
            
            return new ReturnStatement(expression);
        }
        private Statement TryParseAssignmentOrFunctionCall()
        {
            string name = TryParseIdentifier();
            if(name is null)
                return null;

            Statement statement;
            if((statement = TryParseFunctionCall(name)) != null)
                return statement;
            else if((statement = TryParseAssignment(name)) != null)
                return statement;
            else
                throw new UnexpectedToken();            
        }
        private FunctionCall TryParseFunctionCall(string name)
        {
            if(_scanner.CurrentToken.Type != TokenType.PAREN_OPEN)
                return null;
            _scanner.Next();

            IEnumerable<Expression> arguments = TryParseArgumentList() ?? throw new UnexpectedToken();

            if(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
                throw new UnexpectedToken();
            _scanner.Next();

            return new FunctionCall(name, arguments);
        }

        private IEnumerable<Expression> TryParseArgumentList()
        {
            var arguments = new List<Expression>();

            Expression expr = TryParseExpression();
            if(expr is null)
                return arguments;
            arguments.Add(expr);

            while(_scanner.CurrentToken.Type == TokenType.COMMA)
            {
                _scanner.Next();
                expr = TryParseExpression() ?? throw new UnexpectedToken();
                arguments.Add(expr);
            }
            return arguments;
        }
        private Assignment TryParseAssignment(string name)
        {
            DerefVariable derefVar = TryParseDerefVariable(name);
            if(derefVar is null)
                return null;
            
            IOperator assignmentOperator = TryParseOperator() ?? throw new UnexpectedToken();
            _scanner.Next();

            Expression expression = TryParseExpression() ?? throw new UnexpectedToken();

            return new Assignment(name, assignmentOperator as AssignmentOperator, expression);
        }
        private Definition TryParseDefinition()
        {
            string type = TryParseType();
            if(type is null)
                return null;

            string name = TryParseIdentifier() ?? throw new UnexpectedToken();
            Definition def = TryParseDefinition(type, name) ?? throw new UnexpectedToken();

            return def;
        }
        private Conditional TryParseConditional()
        {
            Conditional left = TryParseAndConditional();
            if(left is null)
                return null;
            
            while(_scanner.CurrentToken.Type == TokenType.OR)
            {
                _scanner.Next();
                Conditional right = TryParseAndConditional() ?? throw new UnexpectedToken();
                left = new OrConditional(left, right);
            }
            return left;
        }
        private Conditional TryParseAndConditional()
        {
            Conditional left = TryParseLogical();
            if(left is null)
                return null;
            
            while(_scanner.CurrentToken.Type == TokenType.AND)
            {
                _scanner.Next();
                Conditional right = TryParseLogical() ?? throw new UnexpectedToken();
                left = new AndConditional(left, right);
            }
            return left;
        }
        private Logical TryParseLogical()
        {   
            bool isNegated = _scanner.CurrentToken.Type == TokenType.NOT;
            if(isNegated)
                _scanner.Next();
            Logical logical;
            if((logical = TryParseParenConditional(isNegated)) != null)
                return logical;
            if((logical = TryParseSimpleConditional(isNegated)) != null)
                return logical;
            else
            {
                if(isNegated)
                    throw new UnexpectedToken();
                else
                    return null;
            }
        }
        private SimpleConditional TryParseSimpleConditional(bool isNegated)
        {
            Expression left = TryParseExpression();
            if(left is null)
                return null;

            EqualityOperator equalityOperator = TryParseEqualityOperator() ?? throw new UnexpectedToken();
            Expression right = TryParseExpression() ?? throw new UnexpectedToken();

            return new SimpleConditional(left, right, equalityOperator, isNegated);
        }
        private ParenConditional TryParseParenConditional(bool isNegated)
        {
            if(_scanner.CurrentToken.Type != TokenType.PAREN_OPEN)
                return null;
            _scanner.Next();

            Conditional conditional = TryParseConditional() ?? throw new UnexpectedToken();
            
            if(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
                throw new UnexpectedToken();
            _scanner.Next();

            return new ParenConditional(conditional, isNegated);
        }
        private EqualityOperator TryParseEqualityOperator()
        {
            IOperator equalityOperator = TryParseOperator();
            if(equalityOperator is null)
                return null;
            _scanner.Next();
            if(equalityOperator is EqualityOperator)
                return equalityOperator as EqualityOperator;
            else
                return null;
        }
        private Expression TryParseExpression()
        {
            Expression left = TryParseMultiplicativeExpression();
            if(left is null)
                return null;

            IOperator additiveOperator;
            while((additiveOperator = TryParseOperator()) is AdditiveOperator)
            {
                _scanner.Next();
                Expression right = TryParseMultiplicativeExpression() ?? throw new UnexpectedToken();
                left = new AdditiveExpression(left, right, additiveOperator as AdditiveOperator);
            }
            return left;
        }
        private Expression TryParseMultiplicativeExpression()
        {
            Expression left;
            if((left = TryParseUnary()) == null)
                return null;

            IOperator multiplicativeOperator;
            while((multiplicativeOperator = TryParseOperator()) is MultiplicativeOperator)
            {
                _scanner.Next();
                Expression right = TryParseUnary() ?? throw new UnexpectedToken();
                left = new MultiplicativeExpression(left, right, multiplicativeOperator as MultiplicativeOperator);
            }
            return left;
        }
        private UnaryExpression TryParseUnary()
        {
            bool isNegated = _scanner.CurrentToken.Type == TokenType.MINUS;
            if(isNegated)
                _scanner.Next();
            
            PrimaryExpression expression;
            if((expression = TryParsePrimaryExpression()) != null)
                return new UnaryExpression(expression, isNegated);
            else if(isNegated)
                throw new UnexpectedToken();
            else
                return null;
        }
        private PrimaryExpression TryParsePrimaryExpression()
        {
            PrimaryExpression expression;
            if((expression = TryParseConstantExpression()) != null)
                return expression;
            else if((expression = TryParseDerefVarOrFunCall()) != null)
                return expression;
            else if((expression = TryParseParenExpression()) != null)
                return expression;
            else 
                return null;
        }
        private ConstantExpression TryParseConstantExpression()
        {
            Value value = TryParseValue();
            if(value is null)
                return null;
            
            return new ConstantExpression(value);
        }
        private PrimaryExpression TryParseDerefVarOrFunCall()
        {
            string name = TryParseIdentifier();
            if(name is null)
                return null;

            PrimaryExpression expression;
            if((expression = TryParseFunctionCall(name)) != null)
                return expression;
            else if((expression = TryParseDerefVariable(name)) != null)
                return expression;
            else
                throw new UnexpectedToken();
        }
        private DerefVariable TryParseDerefVariable(string name)
        {
            if(_scanner.CurrentToken.Type != TokenType.BRACKET_OPEN)
                return new DerefVariable(name);
            _scanner.Next();

            Ast.Range left = TryParseRange() ?? throw new UnexpectedToken();

            if(_scanner.CurrentToken.Type == TokenType.COMMA)
                throw new UnexpectedToken();
            _scanner.Next();

            Ast.Range right = TryParseRange() ?? throw new UnexpectedToken();

            if(_scanner.CurrentToken.Type == TokenType.BRACKET_CLOSE)
                throw new UnexpectedToken();
            _scanner.Next();

            return new DerefVariable(name, left, right);
        }
        private Ast.Range TryParseRange()
        {
            Expression first = TryParseExpression();
            if(first is null)
                return null;

            if(_scanner.CurrentToken.Type != TokenType.COLON)
                throw new UnexpectedToken();
            _scanner.Next();

            Expression second = TryParseExpression() ?? throw new UnexpectedToken();

            return new Ast.Range(first, second);
        }
        private ParenExpression TryParseParenExpression()
        {
            if(_scanner.CurrentToken.Type != TokenType.PAREN_OPEN)
                return null;
            _scanner.Next();

            Expression expression = TryParseExpression() ?? throw new UnexpectedToken();
            
            if(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
                throw new UnexpectedToken();
            _scanner.Next();

            return new ParenExpression(expression);
        }
        private Value TryParseValue()
        {
            Value value;
            if(_scanner.CurrentToken.Type == TokenType.NUMBER)
                value = new Int_t((_scanner.CurrentToken as Number).Value);
            else if(_scanner.CurrentToken.Type == TokenType.TEXT)
                value = new String_t(_scanner.CurrentToken.ToString());
            else    
                return null;

            _scanner.Next();
            return value;
        }
        private IOperator TryParseOperator()
        {
            if(!_operatorsMapper.ContainsKey(_scanner.CurrentToken.Type))
                return null;
            IOperator value = _operatorsMapper[_scanner.CurrentToken.Type]();
            return value;
        }
    }
}