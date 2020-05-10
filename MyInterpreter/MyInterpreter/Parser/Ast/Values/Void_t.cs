namespace MyInterpreter.Parser.Ast.Values
{
    public class Void_t : Value
    {
        public ValueType Type { get; private set; }
        public Void_t() => Type = ValueType.Void;
    }
}