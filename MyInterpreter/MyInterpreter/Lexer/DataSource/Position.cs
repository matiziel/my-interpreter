namespace MyInterpreter.Lexer.DataSource
{
    public class Position
    {
        public uint Column { get; private set; }
        public uint Row { get; private set; }

        public Position(uint row, uint column)
        {
            Column = column;
            Row = row;
        }
    }
}