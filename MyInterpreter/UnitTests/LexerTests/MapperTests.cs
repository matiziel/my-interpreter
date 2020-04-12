using System;
using MyInterpreter.Lexer.DataSource;
using Xunit;

namespace UnitTests.LexerTests
{
    class TestSource : ISource
    {
        public char CurrentChar => throw new NotImplementedException();

        public void Next()
        {
            throw new NotImplementedException();
        }
    }
    public class LexerTests
    {
        [Fact]
        public void Test1()
        {

        }
    }
}
