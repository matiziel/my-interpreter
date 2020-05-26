using System.Text;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast {
    public class Parameter  {
        private TypeValue type;
        private string name;
        public Parameter(TypeValue type, string name) {
            this.type = type;
            this.name = name;
        }
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(type);
            sb.Append("->");
            sb.Append(name);
            return sb.ToString();
        }

    }
}