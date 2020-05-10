namespace MyInterpreter.Parser.Ast.Values
{
    public class Matrix_t : Value
    {
        public ValueType Type { get; private set; }
        public Matrix_t()
        {
            Type = ValueType.Matrix;
        }
    }
}