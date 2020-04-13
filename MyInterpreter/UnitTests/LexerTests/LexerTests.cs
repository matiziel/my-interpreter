using System;
using MyInterpreter.Lexer;
using MyInterpreter.Lexer.DataSource;
using MyInterpreter.Lexer.Tokens;
using Xunit;

namespace UnitTests.LexerTests
{
    class TestSource : ISource
    {
        private string sourceString;
        private int position;
        public TestSource (string source)
        {
            this.sourceString = source;
            this.position = 0;
        }
        public char CurrentChar 
        { 
            get 
            {
                if(position >= sourceString.Length)
                    return '\0';
                return sourceString[position]; 
            }
        }
        public void Next() => ++position;
    }
    public class LexerTests
    {
        [Theory]
        [InlineData("xyz + abc", new TokenType[]{ TokenType.IDENTIFIER, TokenType.PLUS, TokenType.IDENTIFIER, TokenType.EOT})]
        [InlineData("xyz += abc", new TokenType[]{ TokenType.IDENTIFIER, TokenType.PLUS_ASSIGN, TokenType.IDENTIFIER, TokenType.EOT })]
        [InlineData("xyz && abc", new TokenType[]{ TokenType.IDENTIFIER, TokenType.AND, TokenType.IDENTIFIER, TokenType.EOT })]
        public void CheckVariousTokens_FromString(string text, TokenType[] tokens)
        {
            var scanner = new Scanner(new TestSource(text));
            foreach(var token in tokens)
            {
                scanner.Next();
                Assert.Equal(token, scanner.CurrentToken.Type);
            }
        }
        [Theory]
        [InlineData("+", TokenType.PLUS)] [InlineData("+=", TokenType.PLUS_ASSIGN)]
        [InlineData("-", TokenType.MINUS)] [InlineData("-=", TokenType.MINUS_ASSIGN)]
        [InlineData("*", TokenType.MULTIPLY)] [InlineData("*=", TokenType.MULTIPLY_ASSIGN)]
        [InlineData("/", TokenType.DIVIDE)] [InlineData("/=", TokenType.DIVIDE_ASSIGN)]
        [InlineData("=", TokenType.ASSIGN)] [InlineData("==", TokenType.EQUALS)]
        [InlineData("<", TokenType.LESS)] [InlineData("<=", TokenType.LESS_EQUAL)]
        [InlineData(">", TokenType.GREATER)] [InlineData(">=", TokenType.GREATER_EQUAL)]
        [InlineData("!", TokenType.NOT)] [InlineData("!=", TokenType.NOT_EQUAL)]
        [InlineData("&&", TokenType.AND)] [InlineData("||", TokenType.OR)]
        public void CheckOperatorToken_FromString(string text, TokenType token)
        {
            var scanner = new Scanner(new TestSource(text));
            scanner.Next();
            Assert.Equal(token, scanner.CurrentToken.Type);
            scanner.Next();
            Assert.Equal(TokenType.EOT, scanner.CurrentToken.Type);
        }
        
    }
}
