namespace MyInterpreter.Parser.Ast.Values
{
    public interface Value 
    { 
        ValueType Type { get; }
    }
    public enum ValueType 
    {
        Int, String, Void, Matrix
    }
}