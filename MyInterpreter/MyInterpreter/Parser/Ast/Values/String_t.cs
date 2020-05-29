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
        public override string ToString() => Value;

        public override bool Equals(object obj) =>
            Value.Equals(obj);

        public override int GetHashCode() =>
            Value.GetHashCode();
        
        public static String_t operator +(String_t a, String_t b) =>
            new String_t(a.Value + b.Value);
        public static bool operator ==(String_t a, String_t b) =>   
            a.Value == b.Value;
        public static bool operator !=(String_t a, String_t b) => 
            a.Value != b.Value;
    }
}