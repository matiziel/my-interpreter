using System.Collections.Generic;
using System;

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
        public Token GetNext()
        {
            throw new NotImplementedException();
        }
    }
}