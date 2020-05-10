namespace MyInterpreter.Parser.Ast.Values
{
    public class String_t : Value
    {
        public string Value { get; private set; }
        public ValueType Type { get; private set; }
        public String_t(string value)
        {
            Value = value;
            Type = ValueType.String;
        }

    }
}