using System;
using System.Text;

namespace MyInterpreter.Parser.Ast.Values {
    public class String_t : Value {
        public string Value { get; private set; }
        public TypeValue Type { get; private set; }
        public String_t(string value) {
            Value = value;
            Type = TypeValue.String;
        }
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(Value);
            return sb.ToString();
        }

    }
}