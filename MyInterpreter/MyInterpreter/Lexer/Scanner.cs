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
        private readonly Dictionary<string, TokenType> keywords;
        private readonly Dictionary<string, TokenType> operators;
        private readonly List<Token> tokens;
        
        public Scanner(ISource source)
        {
            _source = source;
            tokens = new List<Token>();
            keywords = new Dictionary<string, TokenType>();
            InitKeywords();
            operators = new Dictionary<string, TokenType>();
        }
        private void InitKeywords()
        {
            keywords.Add("while", TokenType.WHILE);
            keywords.Add("for", TokenType.FOR);
            keywords.Add("if", TokenType.IF);
            keywords.Add("else", TokenType.ELSE);
            keywords.Add("return", TokenType.RETURN);
        }
        private void InitOperators()
        {
            operators.Add("=", TokenType.ASSIGN); operators.Add("==", TokenType.EQUALS);
            operators.Add(">", TokenType.GREATER); operators.Add(">=", TokenType.GREATER);
            operators.Add("<", TokenType.LESS); operators.Add("<=", TokenType.LESS_EQUAL);
            operators.Add("!", TokenType.NOT); operators.Add("!=", TokenType.NOT_EQUAL);
            operators.Add("+", TokenType.PLUS); operators.Add("+=", TokenType.PLUS_ASSIGN);
            operators.Add("-", TokenType.MINUS); operators.Add("-=", TokenType.MINUS_ASSIGN);
            operators.Add("*", TokenType.MULTIPLY); operators.Add("*=", TokenType.MULTIPLY_ASSIGN); 
            operators.Add("/", TokenType.DIVIDE); operators.Add("/=", TokenType.DIVIDE_ASSIGN); 
            operators.Add("&&", TokenType.AND); operators.Add("||", TokenType.OR);
            operators.Add("%", TokenType.MODULO);

        }
        public Token GetNextToken()
        {
            Token token;
            if ((token = TryToGetIdentifierOrKeyword()) != null)
                return token;
            else if ((token = TryToGetNumber()) != null)
                return token;
            else if ((token = TryToGetOperator()) != null)
                return token;
            else return null;
        }
        private Token TryToGetIdentifierOrKeyword()
        {
            if(!char.IsLetter(_source.Peek))
                return null;

            var sb = new StringBuilder(_source.GetChar());
            while(char.IsLetterOrDigit(_source.Peek) || _source.Peek == '_')
                sb.Append(_source.GetChar());
            
            string name = sb.ToString();

            if(keywords.ContainsKey(name))
                return new Keyword(keywords[name]);
            else
                return new Identifier(name);
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
        private Token TryToGetOperator()
        {
            var withEqualAfter = new List<char>() 
            { 
                '!', '=', '<', '>',
                '+', '-', '*', '/', 
                '%'
            };
            var withoutEqual = new List<char>()
            {
                '|', '&'
            };
            if(!withEqualAfter.Contains(_source.Peek) && !withoutEqual.Contains(_source.Peek))
                return null;

            char c = _source.GetChar();
            var sb = new StringBuilder(c);

            if (withEqualAfter.Contains(c) && _source.Peek == '='
            || withoutEqual.Contains(c) && _source.Peek == c)
                sb.Append(_source.GetChar());

            return new Operator(operators[sb.ToString()], sb.ToString());
        }
    }
}