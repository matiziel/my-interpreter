using System.Text;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast {
    public class Parameter {
        public TypeValue Type { get; private set; }
        public string Name { get; private set; }
        public Parameter(TypeValue type, string name) {
            Type = type;
            Name = name;
        }
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(Type);
            sb.Append("->");
            sb.Append(Name);
            return sb.ToString();
        }

    }
}