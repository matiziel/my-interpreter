using System;
using System.Collections.Generic;
using MyInterpreter.Exceptions.ParserExceptions;
using MyInterpreter.Lexer;
using MyInterpreter.Lexer.Tokens;
using MyInterpreter.Parser.Ast;
using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Statements;

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
                //TODO add parsing definition with initialization
                throw new NotImplementedException();
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
            
            throw new NotImplementedException();
        }
        private FunctionCall TryParseFunctionCall(string name)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        private Expression TryParseExpression()
        {
            throw new NotImplementedException();
        }

    }
}