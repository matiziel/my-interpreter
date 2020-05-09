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
            throw new NotImplementedException();
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
                return null;
            parameters.Add(param);

            while(_scanner.CurrentToken.Type == TokenType.COMMA)  // == comma
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
            Statement statement;
            while((statement = TryParseStatement()) != null)
                statements.Add(statement);
            
            if(_scanner.CurrentToken.Type != TokenType.BRACE_CLOSE)
                throw new UnexpectedToken();

            _scanner.Next();
            return new BlockStatement(statements);
        }
        private Statement TryParseStatement()
        {
            Statement statement = null;
            switch(_scanner.CurrentToken.Type)
            {
                case TokenType.BRACE_OPEN:
                    statement = TryParseBlockStatement();
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