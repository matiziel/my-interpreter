using System;
using System.Collections.Generic;
using System.Text;
using MyInterpreter.Exceptions;
using MyInterpreter.Lexer.DataSource;
using MyInterpreter.Lexer.Tokens;


namespace MyInterpreter.Lexer
{
    public class Scanner : IScanner
    {
        public Token CurrentToken { get; private set; }
        private ISource _source;
        private readonly Dictionary<string, TokenType> keywords;
        private readonly Dictionary<char, Func<ISource, Token>> operators;
        private readonly Dictionary<char, TokenType> literals;
        private readonly int MAX_IDENTIFIER_LENGTH = 128;

        public Scanner(ISource source)
        {
            CurrentToken = null;
            _source = source;
            keywords = Mapper.GetKeywordsMapper();
            operators = Mapper.GetOperatorsMapper();
            literals = Mapper.GetLiteralsMapper();
        }
        public Token Next()
        {
            Token token;
            SkipUnused();
            if ((token = TryToGetIdentifierOrKeyword()) != null)
                CurrentToken = token;
            else if ((token = TryToGetNumber()) != null)
                CurrentToken = token;
            else if ((token = TryToGetString()) != null)
                CurrentToken = token;
            else if ((token = TryToGetOperator()) != null)
                CurrentToken = token;
            else if ((token = TryToGetLiteral()) != null)
                CurrentToken = token;
            else if ((token = TryToGetEndOfText()) != null)
                CurrentToken = token;

            if(token == null)
                throw new UnrecognizedToken(_source.Position, _source.GetLineFromPosition(_source.Position));
            return CurrentToken;  
        }
        private void SkipUnused()
        {
            while(TryToSkipWhiteSpaces() || TryToSkipCommentLine());
        }
        private bool TryToSkipWhiteSpaces()
        {
            if(!char.IsWhiteSpace(_source.CurrentChar))
                return false;

            while(char.IsWhiteSpace(_source.CurrentChar))
                _source.Next();
            return true;
        }
        private bool TryToSkipCommentLine()
        {
            if(_source.CurrentChar != '#')
                return false;

            while(_source.CurrentChar != '\n' && _source.CurrentChar != '\0')
                _source.Next();
            return true;
        }
        private Token TryToGetIdentifierOrKeyword()
        {
            if(!char.IsLetter(_source.CurrentChar))
                return null;
            var sb = new StringBuilder().Append(_source.CurrentChar);
            _source.Next();
            while(char.IsLetterOrDigit(_source.CurrentChar) || _source.CurrentChar == '_')
            {
                sb.Append(_source.CurrentChar);
                if(sb.Length > MAX_IDENTIFIER_LENGTH)
                    throw new TooLongIdentifier(_source.Position, _source.GetLineFromPosition(_source.Position), sb.Length);
                _source.Next();
            }
            string name = sb.ToString();

            if(keywords.ContainsKey(name))
                return new Keyword(keywords[name], _source.Position);
            else
                return new Identifier(name, _source.Position);
        }
        private Token TryToGetNumber()
        {
            if(!char.IsDigit(_source.CurrentChar))
                return null;

            int value = int.Parse(_source.CurrentChar.ToString());
            _source.Next();

            if(value == 0)
                return new Number(value, _source.Position);
                
            while(char.IsDigit(_source.CurrentChar))
            {
                value = value * 10 + int.Parse(_source.CurrentChar.ToString());
                _source.Next();
            }            
            return new Number(value, _source.Position);
        }
        private Token TryToGetString()
        {
            if(_source.CurrentChar != '\"')
                return null;
            
            _source.Next();
            var sb = new StringBuilder();
            while(_source.CurrentChar != '\n' && _source.CurrentChar != '\"')
            {
                sb.Append(_source.CurrentChar);
                _source.Next();       
            }
            if(_source.CurrentChar == '\"')
            {
                _source.Next();
                return new Text(sb.ToString(), _source.Position);
            }
            else
                return null;
        }
        private Token TryToGetOperator()
        {
            if(!operators.ContainsKey(_source.CurrentChar))
                return null;

            Token mappedOperator = (operators[_source.CurrentChar](_source));
            return mappedOperator;
        }
        private Token TryToGetLiteral()
        {
            if(!literals.ContainsKey(_source.CurrentChar))
                return null;
            
            char literal = _source.CurrentChar;;
            _source.Next();
            return new Literal(literals[literal], literal.ToString(), _source.Position);
            
        }
        private Token TryToGetEndOfText()
        {
            if(_source.CurrentChar == '\0')
                return new EndOfText(_source.Position);
            else 
                return null;
        }
    }
}