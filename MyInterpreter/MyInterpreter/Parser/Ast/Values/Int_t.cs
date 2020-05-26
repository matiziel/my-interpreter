using System;
using System.Text;

namespace MyInterpreter.Parser.Ast.Values {
    public class Int_t : Value {
        public int Value { get; private set; }
        public TypeValue Type { get; private set; }
        public Int_t(int value) {
            Value = value;
            Type = TypeValue.Int;
        }
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(Value);
            return sb.ToString();
        } 
    }
}