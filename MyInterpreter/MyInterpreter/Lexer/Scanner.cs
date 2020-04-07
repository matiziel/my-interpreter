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
        private readonly Dictionary<string, TokenType> operators;

        
        public Scanner(ISource source)
        {
            _source = source;
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
            //TODO map char to lambda
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
            //TODO
            //mapa od char 
            if(!operators.ContainsKey(_source.CurrentChar.ToString()))
                return null;
            char c = _source.CurrentChar;
            var sb = new StringBuilder(c);
            _source.Next();

            if(c != '|' && c != '&')
            {
                if(_source.CurrentChar == '=')
                {
                    sb.Append(_source.CurrentChar);
                    _source.Next();

                }
            }
            else
            {
                if(_source.CurrentChar == c)
                {
                    sb.Append(_source.CurrentChar);
                    _source.Next();
                }
                else
                    return null;
            }
            return new Operator(operators[sb.ToString()], sb.ToString());
        }
    }
}