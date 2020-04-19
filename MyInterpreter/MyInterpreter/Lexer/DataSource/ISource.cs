namespace MyInterpreter.Lexer.DataSource
{
    public interface ISource
    {
        char CurrentChar { get; }
        TextPosition Position { get; }
        void Next();
        string GetLineFromPosition(TextPosition position);
    }
}