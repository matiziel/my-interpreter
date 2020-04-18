using MyInterpreter.Lexer.DataSource;

namespace UnitTests.LexerTests
{
    class StringSource : ISource
    {
        private string sourceString;
        public TextPosition Position { get; private set; }
        public StringSource (string source)
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
        public string GetPieceOfText(TextPosition position, int leftShift, int rightShift)
        {
            int begin = (position.SourcePosition - leftShift >= 0) ? position.SourcePosition - leftShift : 0;
            int count = (begin + leftShift + rightShift < sourceString.Length) ? leftShift + rightShift : sourceString.Length - begin - 1; 
            return sourceString.Substring(begin, count);
        }
    }
}