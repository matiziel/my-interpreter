namespace MyInterpreter.Lexer.DataSource
{
    public interface ISource
    {
        char CurrentChar { get; }
        void Next();
    }
}