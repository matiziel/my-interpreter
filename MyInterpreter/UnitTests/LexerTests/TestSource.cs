using MyInterpreter.Lexer.DataSource;

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
}