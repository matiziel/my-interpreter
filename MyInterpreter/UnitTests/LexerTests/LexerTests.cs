using System;
using System.IO;
using MyInterpreter.Exceptions;
using MyInterpreter.Lexer;
using MyInterpreter.DataSource;
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
        [InlineData("(", TokenType.PAREN_OPEN)] [InlineData(")", TokenType.PAREN_CLOSE)]
        [InlineData("[", TokenType.BRACKET_OPEN)] [InlineData("]", TokenType.BRACKET_CLOSE)]
        [InlineData("{", TokenType.BRACE_OPEN)] [InlineData("}", TokenType.BRACE_CLOSE)]
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
        public void CheckTokensTypes_FromFile()
        {
            string path = MakeTestFile();
            TokenType[] tokenTypes = new TokenType[] {
                TokenType.IDENTIFIER, TokenType.IDENTIFIER,
                TokenType.PAREN_OPEN, TokenType.PAREN_CLOSE,
                TokenType.BRACE_OPEN, TokenType.IDENTIFIER,
                TokenType.IDENTIFIER, TokenType.ASSIGN,
                TokenType.NUMBER, TokenType.PLUS,
                TokenType.NUMBER, TokenType.SEMICOLON, 
                TokenType.BRACE_CLOSE, TokenType.EOT
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
        [Fact]
        public void CheckTokensValues_FromFile()
        {
            string path = MakeTestFile();
            string[] values = new string[] {
                "int", "main", "(", ")",
                "{", "int", "x", "=", "1", "+", "2", ";", "}",
            };
            using (var source = new FileSource(path))
            {
                var scanner = new Scanner(source);
                foreach(var value in values)
                {
                    scanner.Next();
                    Assert.Equal(value, scanner.CurrentToken.ToString());
                }
            }
        }
        [Fact]
        public void CheckTokensPositions_FromFile()
        {
            string path = MakeTestFile();
            TextPosition[] positions = new TextPosition[] {
                new TextPosition(3, 1, 18), new TextPosition(3, 5, 22), new TextPosition(3, 9, 26), 
                new TextPosition(3, 10, 27), new TextPosition(4, 1, 29), new TextPosition(5, 5 ,35),
                new TextPosition(5, 9, 39), new TextPosition(5, 11, 41), new TextPosition(5 ,13, 43),
                new TextPosition(5, 15, 45), new TextPosition(5, 17, 47), new TextPosition(5, 18, 48),
                new TextPosition(6, 1, 67), new TextPosition(7, 1, 69), 
            };
            using (var source = new FileSource(path))
            {
                var scanner = new Scanner(source);
                foreach(var position in positions)
                {
                    scanner.Next();
                    Assert.Equal(position.SourcePosition, scanner.CurrentToken.Position.SourcePosition);
                    Assert.Equal(position.Row, scanner.CurrentToken.Position.Row);
                    Assert.Equal(position.Column, scanner.CurrentToken.Position.Column);
                }
            }
        }

        private string MakeTestFile()
        {
            string path = "../../../TestFiles/testfile.ml";
            string[] lines = { 
                "# test comment 1", "", 
                "int main()", "{",
                "    int x = 1 + 2; # test comment 2", "}"
            };
            using (StreamWriter outputFile = new StreamWriter(path))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
            return path;
        }


    }
}
