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
        public string GetLineFromPosition(TextPosition position)
        {
            int begin = position.SourcePosition - position.Column + 1;
            int count = sourceString.Length;
            return sourceString.Substring(begin, count).Split('\n')[0];
        }
    }
}