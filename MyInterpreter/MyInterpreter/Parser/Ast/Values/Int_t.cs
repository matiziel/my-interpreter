namespace MyInterpreter.Parser.Ast.Values
{
    public class Int_t : Value
    {
        public int Value { get; private set; }
        public ValueType Type { get; private set; }
        public Int_t(int value)
        {
            Value = value;
            Type = ValueType.Int;
        }
    }
}