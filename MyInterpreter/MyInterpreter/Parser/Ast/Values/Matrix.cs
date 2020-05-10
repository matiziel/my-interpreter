namespace MyInterpreter.Parser.Ast.Values
{
    public class Matrix : Value
    {
        public ValueType Type { get; private set; }
        public Matrix()
        {
            Type = ValueType.Matrix;
        }
    }
}