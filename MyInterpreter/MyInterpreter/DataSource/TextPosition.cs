namespace MyInterpreter.DataSource
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
        public TextPosition(TextPosition position, int offset = 0)
        {
            Row = position.Row;
            SourcePosition = position.SourcePosition - offset;
            Column = position.Column - offset; 
        }
        public TextPosition(int row, int column, int sourcePosition)
        {
            Row = row; 
            Column = column;
            SourcePosition = sourcePosition;
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