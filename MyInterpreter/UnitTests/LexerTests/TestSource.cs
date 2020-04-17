using MyInterpreter.Lexer.DataSource;

namespace UnitTests.LexerTests
{
    class TestSource : ISource
    {
        private string sourceString;
        public TextPosition Position { get; private set; }
        public TestSource (string source)
        {
            this.sourceString = source;
            Position = new TextPosition();
        }
        public char CurrentChar 
        { 
            get 
            {
                if(Position.SourcePosition >= sourceString.Length)
                    return '\0';
                return sourceString[Position.SourcePosition]; 
            }
        }
        public void Next() => Position.NextCharacter(CurrentChar);
        public string GetPieceOfText(int leftShift, int rightShift)
        {
            int begin = (Position.SourcePosition - leftShift >= 0) ? Position.SourcePosition - leftShift : 0;
            int count = (Position.SourcePosition + rightShift >= sourceString.Length) ? leftShift + rightShift : sourceString.Length - 1; 
            return sourceString.Substring(begin, count);
        }
    }
}