namespace MyInterpreter.Lexer.DataSource
{
    public class TextPosition
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int SourcePosition { get; private set; }
        public TextPosition()
        {
            SourcePosition = 0;
            Row = Column = 1;
        }
        public TextPosition(TextPosition position)
        {
            SourcePosition = position.SourcePosition;
            Row = position.Row;
            Column = position.Column; 
        }
        public void NextCharacter(char currentCharacter)
        {
            ++SourcePosition;
            if(currentCharacter == '\n')
            {
                Column = 1;
                ++Row;
            }
            else
                ++Column;;
        }
    }
}