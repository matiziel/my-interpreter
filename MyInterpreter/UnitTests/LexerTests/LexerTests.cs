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
        [InlineData("xyz + abc;", new TokenType[]{ TokenType.IDENTIFIER, TokenType.PLUS, TokenType.IDENTIFIER, TokenType.SEMICOLON, TokenType.EOT})]
        [InlineData("xyz += abc;", new TokenType[]{ TokenType.IDENTIFIER, TokenType.PLUS_ASSIGN, TokenType.IDENTIFIER,TokenType.SEMICOLON, TokenType.EOT })]
        [InlineData("xyz && abc;", new TokenType[]{ TokenType.IDENTIFIER, TokenType.AND, TokenType.IDENTIFIER, TokenType.SEMICOLON, TokenType.EOT })]
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
            Assert.IsType<EndOfText>(scanner.CurrentToken);
        }
        [Theory]
        [InlineData("while", TokenType.WHILE)]
        [InlineData("if", TokenType.IF)]
        [InlineData("else", TokenType.ELSE)]
        [InlineData("for", TokenType.FOR)]
        [InlineData("return", TokenType.RETURN)]
        public void CheckKeywordToken_FromString(string text, TokenType token)
        {
            var scanner = new Scanner(new TestSource(text));
            scanner.Next();
            Assert.Equal(token, scanner.CurrentToken.Type);
            Assert.IsType<Keyword>(scanner.CurrentToken);
            scanner.Next();
            Assert.Equal(TokenType.EOT, scanner.CurrentToken.Type);
            Assert.IsType<EndOfText>(scanner.CurrentToken);
        }
        [Theory]
        [InlineData("int")]
        [InlineData("string")]
        [InlineData("abc2")]
        [InlineData("xyz1")]
        [InlineData("SumOfThree")]
        [InlineData("ELO420")]
        public void CheckIdentifierToken_FromString(string text)
        {
            var scanner = new Scanner(new TestSource(text));
            scanner.Next();
            Assert.Equal(TokenType.IDENTIFIER, scanner.CurrentToken.Type);
            Assert.IsType<Identifier>(scanner.CurrentToken);
            scanner.Next();
            Assert.Equal(TokenType.EOT, scanner.CurrentToken.Type);
            Assert.IsType<EndOfText>(scanner.CurrentToken); 
        }
        [Theory]
        [InlineData("0")]
        [InlineData("123")]
        [InlineData("231")]
        [InlineData("1230")]
        [InlineData("2131451")]
        [InlineData("23446")]
        public void CheckNumberToken_FromString(string text)
        {
            var scanner = new Scanner(new TestSource(text));
            scanner.Next();
            Assert.Equal(TokenType.NUMBER, scanner.CurrentToken.Type);
            Assert.IsType<Number>(scanner.CurrentToken);
            scanner.Next();
            Assert.Equal(TokenType.EOT, scanner.CurrentToken.Type);
            Assert.IsType<EndOfText>(scanner.CurrentToken); 
        }
        [Theory]
        [InlineData("\"abcd\"")]
        [InlineData("\"xyzu\"")]
        [InlineData("\"ELOELO\"")]
        [InlineData("\"2137ELO\"")]
        [InlineData("\"qwerty\"")]
        [InlineData("\"1qaz2wsx\"")]
        public void CheckStringToken_FromString(string text)
        {
            var scanner = new Scanner(new TestSource(text));
            scanner.Next();
            Assert.Equal(TokenType.STRING, scanner.CurrentToken.Type);
            Assert.IsType<Text>(scanner.CurrentToken);
            scanner.Next();
            Assert.Equal(TokenType.EOT, scanner.CurrentToken.Type);
            Assert.IsType<EndOfText>(scanner.CurrentToken); 
        }
        [Theory]
        [InlineData("(", TokenType.LEFT_PAREN)] [InlineData(")", TokenType.RIGHT_PAREN)]
        [InlineData("[", TokenType.LEFT_BRACKET)] [InlineData("]", TokenType.RIGHT_BRACKET)]
        [InlineData("{", TokenType.LEFT_BRACE)] [InlineData("}", TokenType.RIGHT_BRACE)]
        [InlineData(":", TokenType.COLON)] [InlineData(";", TokenType.SEMICOLON)]
        [InlineData(",", TokenType.COMMA)]
        public void CheckLiteralToken_FromString(string text, TokenType type)
        {
            var scanner = new Scanner(new TestSource(text));
            scanner.Next();
            Assert.Equal(type, scanner.CurrentToken.Type);
            Assert.IsType<Literal>(scanner.CurrentToken);
            scanner.Next();
            Assert.Equal(TokenType.EOT, scanner.CurrentToken.Type);
            Assert.IsType<EndOfText>(scanner.CurrentToken);
        }
        
    }
}
