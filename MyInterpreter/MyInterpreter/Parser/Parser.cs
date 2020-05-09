using System;
using System.Collections.Generic;
using MyInterpreter.Exceptions.ParserExceptions;
using MyInterpreter.Lexer;
using MyInterpreter.Lexer.Tokens;
using MyInterpreter.Parser.Ast;
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
        private Statement TryParseStatement()
        {
            Statement statement;
            if((statement = TryParseBlockStatement()) != null)
                return statement;
            else if((statement = TryParseWhileStatement()) != null)
                return statement;
            else if((statement = TryParseForStatement()) != null)
                return statement;
            else if((statement = TryParseReturnStatement()) != null)
                return statement;
            else if((statement = TryParseAssignmentOrDefinition()) != null)
                return statement;
            else if((statement = TryParseFunctionCall()) != null)
                return statement;
            return null;
        }
        private Statement TryParseIfStatement()
        {
            throw new NotImplementedException();
        }
        private Statement TryParseWhileStatement()
        {
            throw new NotImplementedException();
        }
        private Statement TryParseForStatement()
        {
            throw new NotImplementedException();
        }
        private Statement TryParseReturnStatement()
        {
            throw new NotImplementedException();
        }
        private Statement TryParseAssignmentOrDefinition()
        {
            throw new NotImplementedException();
        }
        private Statement TryParseFunctionCall()
        {
            throw new NotImplementedException();
        }

    }
}