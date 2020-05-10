namespace MyInterpreter.Parser.Ast.Values
{
    public class String : Value
    {
        public string Value { get; private set; }
        public ValueType Type { get; private set; }
        public String(string value)
        {
            Value = value;
            Type = ValueType.String;
        }

    }
}