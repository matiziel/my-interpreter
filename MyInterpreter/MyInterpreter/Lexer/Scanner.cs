using System;
using System.Collections.Generic;
using System.Text;
using MyInterpreter.Lexer.DataSource;
using MyInterpreter.Lexer.Tokens;

namespace MyInterpreter.Lexer
{
    public class Scanner
    {
        private readonly ISource _source;
        private Dictionary<string, TokenType> keywords;
        public Scanner(ISource source)
        {
            _source = source;
        }

        private void InitScanner()
        {
            keywords = new Dictionary<string, TokenType>();
            keywords.Add("while", TokenType.WHILE);
            keywords.Add("if", TokenType.IF);
            keywords.Add("else", TokenType.ELSE);
            keywords.Add("for", TokenType.FOR);
        }
        public Token GetNextToken()
        {
            Token token = null;

            if((token = TryToGetIdentOrKeyword()) != null)
                return token;
            else if ((token = TryToGetNumber()) != null)
                return token;
            return token;
        }
        private Token TryToGetIdentOrKeyword()
        {
            if(!char.IsLetter(_source.Peek))
                return null;

            var sb = new StringBuilder(_source.GetChar());
            while(char.IsLetterOrDigit(_source.Peek))
                sb.Append(_source.GetChar());
            
            string name = sb.ToString();

            if(keywords.ContainsKey(name))
                return new Keyword(keywords[name]);
            else
                return new Word(TokenType.IDENTIFIER, name);
        }

        private Token TryToGetNumber()
        {
            if(!char.IsNumber(_source.Peek) || _source.Peek == '0')
                return null;
            
            uint value = uint.Parse(_source.GetChar().ToString());
            while(char.IsNumber(_source.Peek))
                value = value * 10 + uint.Parse(_source.GetChar().ToString());
            
            return new Number(value);
        }

    }
}