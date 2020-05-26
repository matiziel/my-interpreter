using System;
using System.Text;

namespace MyInterpreter.Parser.Ast.Values {
    public class Void_t : Value {
        public TypeValue Type { get; private set; }
        public Void_t() => Type = TypeValue.Void;
        
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(Type.ToString());
            return sb.ToString();
        }
    }
}