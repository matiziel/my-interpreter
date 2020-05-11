using MyInterpreter.Lexer;
using MyInterpreter.Lexer.Tokens;

namespace UnitTests.ParserTests
{
    public class TestScanner : IScanner
    {
        private Token[] tokens;
        private int currentIndex;
        public TestScanner(Token[] tokens)
        {
            this.tokens = tokens;
            currentIndex = 0;
        }
        public Token CurrentToken 
        { 
            get 
            {
                if(currentIndex >= tokens.Length)
                    return new EndOfText(new MyInterpreter.DataSource.TextPosition(0,0,0));
                return tokens[currentIndex];
            }
        }
        public void Next() => currentIndex++;

    }
}