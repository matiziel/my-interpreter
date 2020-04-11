namespace MyInterpreter.Lexer.DataSource
{
    public interface ISource
    {
        TextPosition Position { get; }
        char CurrentChar { get; }
        void Next();
    }
}