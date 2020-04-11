using System;
using System.Collections.Generic;
using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    static class Mapper
    {
        public static Dictionary<string, TokenType> GetKeywordsMapper()
        {
            var keywords = new Dictionary<string, TokenType>();
            keywords.Add("while", TokenType.WHILE);
            keywords.Add("for", TokenType.FOR);
            keywords.Add("if", TokenType.IF);
            keywords.Add("else", TokenType.ELSE);
            keywords.Add("return", TokenType.RETURN);
            return keywords;

        }
        public static Dictionary<char, Func<ISource, Token>> GetOperatorsMapper()
        {
            var mapper = new Dictionary<char, Func<ISource, Token>>();
            mapper.Add('=', (src) => {
                if(src.CurrentChar == '=')
                {
                    src.Next();
                    return new Operator(TokenType.EQUALS, "==");
                }
                else
                    return new Operator(TokenType.ASSIGN, "=");
            });
            mapper.Add('<', (src) => {
                if(src.CurrentChar == '=')
                {
                    src.Next();
                    return new Operator(TokenType.LESS_EQUAL, "<=");
                }
                else
                    return new Operator(TokenType.LESS, "<");
            });
            mapper.Add('>', (src) => {
                if(src.CurrentChar == '=')
                {
                    src.Next();
                    return new Operator(TokenType.GREATER_EQUAL, ">=");
                }
                else
                    return new Operator(TokenType.GREATER, ">");
            });
            mapper.Add('!', (src) => {
                if(src.CurrentChar == '=')
                {
                    src.Next();
                    return new Operator(TokenType.NOT_EQUAL, "!=");
                }
                else
                    return new Operator(TokenType.NOT, "!");
            });
            mapper.Add('+', (src) => {
                if(src.CurrentChar == '=')
                {
                    src.Next();
                    return new Operator(TokenType.PLUS_ASSIGN, "+=");
                }
                else
                    return new Operator(TokenType.PLUS, "+");
            });
            mapper.Add('-', (src) => {
                if(src.CurrentChar == '-')
                {
                    src.Next();
                    return new Operator(TokenType.MINUS_ASSIGN, "-=");
                }
                else
                    return new Operator(TokenType.MINUS, "-");
            });
            mapper.Add('*', (src) => {
                if(src.CurrentChar == '=')
                {
                    src.Next();
                    return new Operator(TokenType.MULTIPLY_ASSIGN, "*=");
                }
                else
                    return new Operator(TokenType.MULTIPLY, "*");
            });
            mapper.Add('/', (src) => {
                if(src.CurrentChar == '=')
                {
                    src.Next();
                    return new Operator(TokenType.DIVIDE_ASSIGN, "/=");
                }
                else
                    return new Operator(TokenType.DIVIDE, "/");
            });
            mapper.Add('%', (src) => {
                if(src.CurrentChar == '=')
                {
                    src.Next();
                    return new Operator(TokenType.MODULO_ASSIGN, "%=");
                }
                else
                    return new Operator(TokenType.MODULO, "%");
            });
            mapper.Add('&', (src) => {
                if(src.CurrentChar == '&')
                {
                    src.Next();
                    return new Operator(TokenType.AND, "&&");
                }
                else
                    return null;
            });
            mapper.Add('|', (src) => {
                if(src.CurrentChar == '|')
                {
                    src.Next();
                    return new Operator(TokenType.AND, "||");
                }
                else
                    return null;
            });
            return mapper;
        }
    }
}