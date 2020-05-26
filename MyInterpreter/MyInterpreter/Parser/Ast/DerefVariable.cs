using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;
using System.Text;

namespace MyInterpreter.Parser.Ast {
    public class DerefVariable : PrimaryExpression {
        public string name;
        private Range left;
        private Range right;
        public DerefVariable(string name, Range left = null, Range right = null) {
            this.name = name;
            this.left = left;
            this.right = right;
        }
        public Value Evaluate(ExecEnvironment environment) {
            return environment.GetVariable(name).Value;
        }
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(name);
            return sb.ToString();
        }

    }
}