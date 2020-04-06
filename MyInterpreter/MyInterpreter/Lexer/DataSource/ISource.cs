namespace MyInterpreter.Lexer.DataSource
{
    public interface ISource
    {
        char Peek { get; }
        char GetChar();
    }
}