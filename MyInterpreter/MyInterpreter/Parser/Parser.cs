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
        public Parser(IScanner scanner)
        {
            _scanner = scanner;
            _functions = new Dictionary<string, Function>();
            _definitions = new List<Definition>();
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

            string type, name;

            if((type = TryParseType()) == null)
                throw new UnexpectedToken();
            
            if((name = TryParseIdentifier()) == null)
                throw new UnexpectedToken();
                
            Definition def;
            Function fun;
            if((def = TryParseDefinition(type, name)) != null)
                _definitions.Add(def);
            else if((fun = TryParseFunction(type, name)) != null)
                _functions.Add(name, fun);
            else 
                throw new UnexpectedToken();

            return true;
        }
        private string TryParseType()
        {
            string typename;
            if((typename = TryToGetType(_scanner.CurrentToken)) == null)
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
            if(_scanner.CurrentToken.Type == TokenType.SEMICOLON)
            {
                _scanner.Next();
                return new Definition(type, name);
            }
            else if(_scanner.CurrentToken.Type == TokenType.ASSIGN)
            {
                _scanner.Next();
                Expression expr;
                if((expr = TryParseExpression()) == null)
                    throw new UnexpectedToken();

                if(_scanner.CurrentToken.Type == TokenType.SEMICOLON)
                    throw new UnexpectedToken();
                
                _scanner.Next();
                return new Definition(type, name, expr);
            }
            else
                return null;

        }
        private Function TryParseFunction(string type, string name)
        {
            if(_scanner.CurrentToken.Type != TokenType.PAREN_OPEN)
                return null;
            _scanner.Next();

            IEnumerable<Parameter> parameters;
            if((parameters = TryParseParameterList()) == null)
                throw new UnexpectedToken();
            
            if(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
                throw new UnexpectedToken();
            _scanner.Next();

            BlockStatement statement;
            if((statement = TryParseBlockStatement()) == null)
                throw new UnexpectedToken();

            return new Function(type, name, parameters, statement);
        }
        private List<Parameter> TryParseParameterList()
        {
            var parameters = new List<Parameter>();
            Parameter param;

            if((param = TryParseParameter()) == null)
                return parameters;
            parameters.Add(param);

            while(_scanner.CurrentToken.Type == TokenType.COMMA) 
            {
                _scanner.Next();
                if((param = TryParseParameter()) == null)
                    throw new UnexpectedToken();
                parameters.Add(param);
            }
            return parameters;
        }
        private Parameter TryParseParameter()
        {
            string type, name;
            if((type = TryParseType()) == null)
                return null;
            if((name = TryParseIdentifier()) == null)
                throw new UnexpectedToken();
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
                return statement;
            else if((statement = TryParseAssignmentOrFunctionCall()) != null)
                return statement;
            else if((statement = TryParseDefinition()) != null)
                return statement;
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

            Conditional conditional;
            if((conditional = TryParseConditional()) == null)
                throw new UnexpectedToken();

            if(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
                throw new UnexpectedToken();
            _scanner.Next();

            Statement statementIf;
            if((statementIf = TryParseStatement()) == null)
                throw new UnexpectedToken();

             if(_scanner.CurrentToken.Type != TokenType.ELSE)
                return new IfStatement(conditional, statementIf);
            _scanner.Next();

            Statement statementElse;
            if((statementElse = TryParseStatement()) == null)
                throw new UnexpectedToken();
            
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

            Conditional conditional;
            if((conditional = TryParseConditional()) == null)
                throw new UnexpectedToken();

            if(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
                throw new UnexpectedToken();
            _scanner.Next();

            Statement statement;
            if((statement = TryParseStatement()) == null)
                throw new UnexpectedToken();

            return new WhileStatement(conditional, statement);   
        }
        private Statement TryParseForStatement()
        {
            throw new NotImplementedException();
        }
        private Statement TryParseReturnStatement()
        {
            if(_scanner.CurrentToken.Type != TokenType.RETURN)
                return null;
            _scanner.Next();

            Expression expression;
            if((expression = TryParseExpression()) == null)
                throw new UnexpectedToken();
            
            return new ReturnStatement(expression);
        }
        private Statement TryParseAssignmentOrFunctionCall()
        {
            string name;
            if((name = TryParseIdentifier()) == null)
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

            IEnumerable<Expression> arguments;
            if((arguments = TryParseArgumentList()) == null)
                throw new UnexpectedToken();

            return new FunctionCall(name, arguments);
        }

        private IEnumerable<Expression> TryParseArgumentList()
        {
            var arguments = new List<Expression>();
            Expression expr;

            if((expr = TryParseExpression()) == null)
                return arguments;
            arguments.Add(expr);

            while(_scanner.CurrentToken.Type == TokenType.COMMA)
            {
                _scanner.Next();
                if((expr = TryParseExpression()) == null)
                    throw new UnexpectedToken();
                arguments.Add(expr);
            }
            return arguments;
        }
        private Assignment TryParseAssignment(string name)
        {
            throw new NotImplementedException();
        }
        private Definition TryParseDefinition()
        {
            string type;
            if((type = TryParseType()) == null)
                return null;
            string name;
            if((name = TryParseIdentifier()) == null)
                throw new UnexpectedToken();
            Definition def;
            if((def = TryParseDefinition(type, name)) == null)
                throw new UnexpectedToken();
            
            return def;
        }
        private Conditional TryParseConditional()
        {
            Conditional left;
            if((left = TryParseAndConditional()) == null)
                return null;
            
            while(_scanner.CurrentToken.Type == TokenType.OR)
            {
                Conditional right;
                if((right = TryParseAndConditional()) == null)
                    throw new UnexpectedToken();
                left = new OrConditional(left, right);
            }
            return left;
        }
        private Conditional TryParseAndConditional()
        {
            Conditional left;
            if((left = TryParseLogical()) == null)
                return null;
            
            while(_scanner.CurrentToken.Type == TokenType.OR)
            {
                Conditional right;
                if((right = TryParseLogical()) == null)
                    throw new UnexpectedToken();
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
            if((logical = TryParseSimpleConditional(isNegated)) != null)
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
            Expression left, right;
            EqualityOperator equalityOperator;

            if((left = TryParseExpression()) == null)
                return null;

            if((equalityOperator = TryParseEqualityOperator()) == null)
                throw new UnexpectedToken();
            if((right = TryParseExpression()) == null)
                throw new UnexpectedToken();

            return new SimpleConditional(left, right, equalityOperator, isNegated);
        }
        private ParenConditional TryParseParenConditional(bool isNegated)
        {
            if(_scanner.CurrentToken.Type != TokenType.PAREN_OPEN)
                return null;
            _scanner.Next();

            Conditional conditional;
            if((conditional = TryParseConditional()) == null)
                throw new UnexpectedToken();
            
            if(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
                throw new UnexpectedToken();
            _scanner.Next();

            return new ParenConditional(conditional, isNegated);
        }
        private EqualityOperator TryParseEqualityOperator()
        {
            throw new NotImplementedException();
        }
        private Expression TryParseExpression()
        {
            Expression left;
            if((left = TryParseMultiplicativeExpression()) == null)
                return null;

            IOperator additiveOperator;
            while((additiveOperator = TryParseOperator()) is AdditiveOperator)
            {
                Expression right;
                if((right = TryParseMultiplicativeExpression()) == null)
                    throw new UnexpectedToken();
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
                Expression right;
                if((right = TryParseUnary()) == null)
                    throw new UnexpectedToken();
                left = new MultiplicativeExpression(left, right, multiplicativeOperator as MultiplicativeOperator);
            }
            return left;
        }
        private Expression TryParseUnary()
        {
            bool isNegative = _scanner.CurrentToken.Type == TokenType.MINUS;
            if(isNegative)
                _scanner.Next();

            
            
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

    }
}