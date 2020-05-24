namespace MyInterpreter.Parser.Ast.Operators
{
    public interface IOperator : Node
    {
        string Operator { get; }
    }
}