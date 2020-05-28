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
        
        public override bool Equals(object obj) {
            return Value.Equals(obj);
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }
        public static Int_t operator +(Int_t a, Int_t b) => new Int_t(a.Value + b.Value);
        public static Int_t operator -(Int_t a, Int_t b) => new Int_t(a.Value - b.Value);
        public static Int_t operator *(Int_t a, Int_t b) => new Int_t(a.Value * b.Value);
        public static Int_t operator -(Int_t a) => new Int_t(-a.Value);
        public static bool operator ==(Int_t a, Int_t b) => a.Value == b.Value;
        public static bool operator !=(Int_t a, Int_t b) => a.Value != b.Value;
    }
}