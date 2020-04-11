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

        
        public Scanner(ISource source)
        {
            CurrentToken = null;
            _source = source;
            keywords = Mapper.GetKeywordsMapper();
            operatorsMapper = Mapper.GetOperatorsMapper();
        }
        public Token GetNextToken()
        {
            Token token;
            SkipUnused();
            if ((token = TryToGetIdentifierOrKeyword()) != null)
                CurrentToken = token;
            else if ((token = TryToGetNumber()) != null)
                CurrentToken = token;
            else if ((token = TryToGetOperator()) != null)
                CurrentToken = token;
            
            return CurrentToken;
        }
        private void SkipUnused()
        {
            while(char.IsWhiteSpace(_source.CurrentChar))
                _source.Next();
        }
        private Token TryToGetIdentifierOrKeyword()
        {
            //TODO limit length of identifier
            if(!char.IsLetter(_source.CurrentChar))
                return null;
            var sb = new StringBuilder(_source.CurrentChar);
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
            //TODO
            //json.org
            if(!char.IsDigit(_source.CurrentChar) || _source.CurrentChar == '0')
                return null;
            
            uint value = uint.Parse(_source.CurrentChar.ToString());
            _source.Next();
            while(char.IsDigit(_source.CurrentChar))
            {
                value = value * 10 + uint.Parse(_source.CurrentChar.ToString());
                _source.Next();
            }            
            return new Number(value);
        }
        private Token TryToGetString()
        {
            throw new NotImplementedException();   
        }
        private Token TryToGetOperator()
        {
            if(!operatorsMapper.ContainsKey(_source.CurrentChar))
                return null;

            return operatorsMapper[_source.CurrentChar](_source);
        }
    }
}