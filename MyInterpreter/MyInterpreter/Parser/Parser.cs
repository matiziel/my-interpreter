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
        IDictionary<string, Function> _functions;
        ICollection<Definition> _definitions;
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
                
            Definition definition;
            Function function;
            if((definition = TryParseDefinition(type, name)) != null)
                _definitions.Add(definition);
            else if((function = TryParseFunction(type, name)) != null)
                _functions.Add(name, function);
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
                && token.Type != TokenType.VOID && token.Type != TokenType.MATRIX
            )
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
            throw new NotImplementedException();
        }
        private Function TryParseFunction(string type, string name)
        {
            throw new NotImplementedException();
        }
        private List<Parameter> ParseParameterList()
        {
            List<Parameter> parameters = new List<Parameter>();
            if(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
                parameters.Add(new Parameter(ParseType(), ParseIdentifier()));

            while(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)  // == comma
            {
                if(_scanner.CurrentToken.Type != TokenType.COMMA)
                    throw new UnexpectedToken();
                _scanner.Next();
                parameters.Add(new Parameter(ParseType(), ParseIdentifier()));
            }
            _scanner.Next();
            return parameters;
        }
        private BlockStatement ParseBlockStatement()
        {
            if(_scanner.CurrentToken.Type != TokenType.BRACE_OPEN)
                throw new UnexpectedToken();
            
            _scanner.Next();
            BlockStatement statement = new BlockStatement();
            while(_scanner.CurrentToken.Type != TokenType.BRACE_CLOSE)
            {
                statement.AddStatement(ParseStatement());
            }
            _scanner.Next();
            return statement;
        }
        private Statement ParseStatement()
        {
            Statement statement = null;
            switch(_scanner.CurrentToken.Type)
            {
                case TokenType.BRACE_OPEN:
                    statement = ParseBlockStatement();
                    break;
                case TokenType.IF:
                    statement = ParseIfStatement();
                    break;
                case TokenType.WHILE:
                    statement = ParseWhileStatement();
                    break;
                case TokenType.FOR:
                    statement = ParseForStatement();
                    break;
                case TokenType.RETURN:
                    statement = ParseReturnStatement();
                    break;
                case TokenType.IDENTIFIER:
                    statement = ParseAssignmentOrDefinition();
                    break;
                default:
                    throw new UnexpectedToken();
            }
            return statement;
        }
        private Statement ParseIfStatement()
        {
            throw new NotImplementedException();
        }
        private Statement ParseWhileStatement()
        {
            throw new NotImplementedException();
        }
        private Statement ParseForStatement()
        {
            throw new NotImplementedException();
        }
        private Statement ParseReturnStatement()
        {
            throw new NotImplementedException();
        }
        private Statement ParseAssignmentOrDefinition()
        {
            throw new NotImplementedException();
        }
    }
}