using MyInterpreter.Lexer;
using MyInterpreter.Lexer.Tokens;

namespace UnitTests.ParserTests
{
    public class TestScanner : IScanner
    {
        public Token CurrentToken => throw new System.NotImplementedException();

        public Token Next()
        {
            throw new System.NotImplementedException();
        }
    }
}