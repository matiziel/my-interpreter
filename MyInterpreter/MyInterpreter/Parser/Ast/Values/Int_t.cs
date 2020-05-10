namespace MyInterpreter.Parser.Ast.Values
{
    public class Int_t : Value
    {
        public uint Value { get; private set; }
        public ValueType Type { get; private set; }
        public Int_t(uint value)
        {
            Value = value;
            Type = ValueType.Int;
        }
    }
}