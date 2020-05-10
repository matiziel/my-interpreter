namespace MyInterpreter.Parser.Ast.Values
{
    public class Void : Value
    {
        public ValueType Type { get; private set; }
        public Void() => Type = ValueType.Void;
    }
}