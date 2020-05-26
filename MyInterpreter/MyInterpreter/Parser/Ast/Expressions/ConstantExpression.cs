using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;
using System.Text;

namespace MyInterpreter.Parser.Ast.Expressions {
    public class ConstantExpression : PrimaryExpression {
        private Value value;
        public ConstantExpression(Value value) => this.value = value;
        public Value Evaluate(ExecEnvironment environment) => value;

        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(value.ToString());
            return sb.ToString();
        }
    }
}