using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;
using System.Text;

namespace MyInterpreter.Parser.Ast {
    public class DerefVariable : PrimaryExpression {
        private string name;
        private Range left;
        private Range right;
        public DerefVariable(string name, Range left = null, Range right = null) {
            this.name = name;
            this.left = left;
            this.right = right;
        }
        public Value Evaluate(ExecEnvironment environment) {
            return GetVariable(environment).Value;
        }
        public Variable GetVariable(ExecEnvironment environment) =>
            environment.GetVariable(name);
        
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(name);
            if (left != null && right != null) {
                sb.Append('[');
                sb.Append(left.ToString());
                sb.Append(',');
                sb.Append(right.ToString());
                sb.Append(']');
            }
            return sb.ToString();
        }

    }
}