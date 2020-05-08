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

        public Parser(IScanner scanner)
        {
            _scanner = scanner;
        }

        public Program Parse()
        {
            var program = new Program();
            _scanner.Next();
            while(_scanner.CurrentToken.Type != TokenType.EOT)
            {
                ParseDefinitionsOrFunctionDefs(program);
            }
            return program;
        }

        private void ParseDefinitionsOrFunctionDefs(Program program)
        {
            string type = GetTypeName();
            string name = GetIdentifier();

            if(_scanner.CurrentToken.Type == TokenType.PAREN_OPEN)
            {
                _scanner.Next();
                var parameters = ParseParameterList();
                _scanner.Next();
                var blockStatement = ParseBlockStatement();
                program.AddFunction(new Function(type, name, blockStatement));
            }
            else if(_scanner.CurrentToken.Type == TokenType.ASSIGN)
            {

            }
            else if(_scanner.CurrentToken.Type == TokenType.SEMICOLON)
                program.AddDefinition(new Definition());
            else
                throw new UnexpectedToken();
        }
        private string GetTypeName()
        {
            string typename;
            if((typename = IfCorrectGetType(_scanner.CurrentToken)) == null)
                throw new UnexpectedToken();
            _scanner.Next();
            return typename;
        }
        private string IfCorrectGetType(Token token)
        {
            if(token.Type != TokenType.IDENTIFIER)
                return null;
            
            if(token.ToString() != "int" && token.ToString() != "string" && token.ToString() != "matrix")
                return null;
            
            return token.ToString();
        }
        private string GetIdentifier()
        {
            string name;
            if(_scanner.CurrentToken.Type != TokenType.IDENTIFIER)
                throw new UnexpectedToken();
            name = _scanner.CurrentToken.ToString();
            _scanner.Next();
            return name;
        }
        private List<Parameter> ParseParameterList()
        {
            List<Parameter> parameters = new List<Parameter>();
            while(_scanner.CurrentToken.Type != TokenType.PAREN_CLOSE)
            {
                parameters.Add(new Parameter(GetTypeName(), GetIdentifier()));
                if(_scanner.CurrentToken.Type != TokenType.COMMA)
                    throw new UnexpectedToken();
                _scanner.Next();
            }
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
            return statement;
        }
        private Statement ParseStatement()
        {
            throw new NotImplementedException();
        }
    }
}