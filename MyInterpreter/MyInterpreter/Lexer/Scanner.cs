using System;
using System.Collections.Generic;
using System.Text;
using MyInterpreter.Lexer.DataSource;
using MyInterpreter.Lexer.Tokens;

namespace MyInterpreter.Lexer
{
    public class Scanner
    {
        public Token CurrentToken { get; private set; }
        private readonly ISource _source;
        private readonly Dictionary<string, TokenType> keywords;
        private readonly Dictionary<char, Func<ISource, Token>> operatorsMapper;
        private readonly Dictionary<char, TokenType> literals;

        
        public Scanner(ISource source)
        {
            CurrentToken = null;
            _source = source;
            keywords = Mapper.GetKeywordsMapper();
            operatorsMapper = Mapper.GetOperatorsMapper();
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

            return CurrentToken; // TODO throw exception with unrecognized token 
        }
        private void SkipUnused()
        {
            //TODO skip comments
            while(char.IsWhiteSpace(_source.CurrentChar))
                _source.Next();
        }
        private Token TryToGetIdentifierOrKeyword()
        {
            //TODO limit length of identifier
            if(!char.IsLetter(_source.CurrentChar))
                return null;
            var sb = new StringBuilder().Append(_source.CurrentChar);
            _source.Next();
            while(char.IsLetterOrDigit(_source.CurrentChar) || _source.CurrentChar == '_')
            {
                sb.Append(_source.CurrentChar);
                _source.Next();
            }
            string name = sb.ToString();

            if(keywords.ContainsKey(name))
                return new Keyword(keywords[name]);
            else
                return new Identifier(name);
        }
        private Token TryToGetNumber()
        {
            if(!char.IsDigit(_source.CurrentChar))
                return null;

            uint value = uint.Parse(_source.CurrentChar.ToString());
            _source.Next();

            if(value == 0)
                return new Number(value);
                
            while(char.IsDigit(_source.CurrentChar))
            {
                value = value * 10 + uint.Parse(_source.CurrentChar.ToString());
                _source.Next();
            }            
            return new Number(value);
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
                return new Text(sb.ToString());
            }
            else
                return null;
        }
        private Token TryToGetOperator()
        {
            if(!operatorsMapper.ContainsKey(_source.CurrentChar))
                return null;

            Token mappedOperator = (operatorsMapper[_source.CurrentChar](_source));
            return mappedOperator;
        }
        private Token TryToGetLiteral()
        {
            if(!literals.ContainsKey(_source.CurrentChar))
                return null;
            
            Token literal = new Literal(literals[_source.CurrentChar], _source.CurrentChar.ToString());
            _source.Next();
            return literal;
        }
        private Token TryToGetEndOfText()
        {
            if(_source.CurrentChar == '\0')
                return new EndOfText();
            else 
                return null;
        }
    }
}