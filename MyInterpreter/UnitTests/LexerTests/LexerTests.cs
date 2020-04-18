using System;
using MyInterpreter.Exceptions;
using MyInterpreter.Lexer;
using MyInterpreter.Lexer.DataSource;
using MyInterpreter.Lexer.Tokens;
using Xunit;

namespace UnitTests.LexerTests
{
    public class LexerTests
    {
        [Theory]
        [InlineData("xyz + abc;", new TokenType[] { TokenType.IDENTIFIER, TokenType.PLUS, TokenType.IDENTIFIER, TokenType.SEMICOLON, TokenType.EOT})]
        [InlineData("xyz += abc;", new TokenType[]{ TokenType.IDENTIFIER, TokenType.PLUS_ASSIGN, TokenType.IDENTIFIER,TokenType.SEMICOLON, TokenType.EOT })]
        [InlineData("xyz && abc;", new TokenType[]{ TokenType.IDENTIFIER, TokenType.AND, TokenType.IDENTIFIER, TokenType.SEMICOLON, TokenType.EOT })]
        public void CheckVariousTokens_FromString(string text, TokenType[] tokens)
        {
            var scanner = new Scanner(new StringSource(text));
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
            var scanner = new Scanner(new StringSource(text));
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
            var scanner = new Scanner(new StringSource(text));
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
            var scanner = new Scanner(new StringSource(text));
            scanner.Next();
            Assert.Equal(TokenType.IDENTIFIER, scanner.CurrentToken.Type);
            Assert.IsType<Identifier>(scanner.CurrentToken);
            Assert.Equal(text, (scanner.CurrentToken as Identifier).Value);
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
            var scanner = new Scanner(new StringSource(text));
            scanner.Next();
            Assert.Equal(TokenType.NUMBER, scanner.CurrentToken.Type);
            Assert.IsType<Number>(scanner.CurrentToken);
            Assert.Equal(int.Parse(text), (scanner.CurrentToken as Number).Value);
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
            var scanner = new Scanner(new StringSource(text));
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
            var scanner = new Scanner(new StringSource(text));

            scanner.Next();
            Assert.Equal(type, scanner.CurrentToken.Type);
            Assert.IsType<Literal>(scanner.CurrentToken);

            scanner.Next();
            Assert.Equal(TokenType.EOT, scanner.CurrentToken.Type);
            Assert.IsType<EndOfText>(scanner.CurrentToken);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData("#sdkasldkkadkk\n#sadjjadjkad")]
        [InlineData("#####kmdas")]
        [InlineData("#sajdj\n\t#sdada")]
        [InlineData("#sajdj\n\n\n#fagsgdfdhh")]
        public void CheckEndOfTextWhiteSpacesAndComments_FromString(string text)
        {
            var scanner = new Scanner(new StringSource(text));
            scanner.Next();
            Assert.Equal(TokenType.EOT, scanner.CurrentToken.Type);
            Assert.IsType<EndOfText>(scanner.CurrentToken);
        }
        [Theory]
        [InlineData("@2332")]
        [InlineData("^66623")]
        [InlineData("?@323243")]
        [InlineData("$$$$$$$")]
        [InlineData("''''''''''''")]
        [InlineData("```")]
        public void CheckUnrecognizedToken_FromString(string text)
        {
            var scanner = new Scanner(new StringSource(text));
            Assert.Throws<UnrecognizedToken>(() => scanner.Next());
        }

        [Fact]
        public void CheckTokens_FromFile()
        {
            string path = "../../../TestFiles/testfile.ml";
            TokenType[] tokenTypes = new TokenType[] {
                TokenType.IDENTIFIER, TokenType.IDENTIFIER,
                TokenType.LEFT_PAREN, TokenType.IDENTIFIER,
                TokenType.IDENTIFIER, TokenType.COMMA,
                TokenType.IDENTIFIER, TokenType.IDENTIFIER,
                TokenType.RIGHT_PAREN, TokenType.LEFT_BRACE,
                TokenType.RETURN, TokenType.IDENTIFIER,
                TokenType.PLUS, TokenType.IDENTIFIER,
                TokenType.SEMICOLON, TokenType.RIGHT_BRACE,
                TokenType.IDENTIFIER, TokenType.IDENTIFIER,
                TokenType.LEFT_PAREN, TokenType.RIGHT_PAREN,
                TokenType.LEFT_BRACE, TokenType.IDENTIFIER,
                TokenType.IDENTIFIER, TokenType.ASSIGN,
                TokenType.IDENTIFIER, TokenType.LEFT_PAREN,
                TokenType.NUMBER, TokenType.COMMA, 
                TokenType.NUMBER, TokenType.RIGHT_PAREN,
                TokenType.SEMICOLON, TokenType.RIGHT_BRACE,
                TokenType.EOT
            };
            using (var source = new FileSource(path))
            {
                var scanner = new Scanner(source);
                foreach(var type in tokenTypes)
                {
                    scanner.Next();
                    Assert.Equal(type, scanner.CurrentToken.Type);
                }
            }
        }
    }
}
