using System.Text;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast {
    public class Variable {
        public TypeValue Type { get; private set; }
        public string Name { get; private set; }
        public Expression First { get; private set; }
        public Expression Second { get; private set; }
        public Value Value { get; set; }
        public Variable(TypeValue type, string name, Expression first = null, Expression second = null) {
            Type = type;
            Name = name;
            First = first;
            Second = second;
        }
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(Type);
            sb.Append("\n");
            sb.Append(Name);
            if (First != null && Second != null) {
                sb.Append('[');
                sb.Append(First.ToString());
                sb.Append(']');
                sb.Append('[');
                sb.Append(Second.ToString());
                sb.Append(']');
            }
            return sb.ToString();
        }

    }
}