using System.Text;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Conditionals {
    public class OrConditional : Conditional {
        private Conditional leftConditional;
        private Conditional rightConditional;
        public OrConditional(Conditional left, Conditional right) {
            leftConditional = left;
            rightConditional = right;
        }
        public bool Evaluate(ExecEnvironment environment)
            => leftConditional.Evaluate(environment) || rightConditional.Evaluate(environment);

        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(leftConditional.ToString());
            sb.Append(" || ");
            sb.Append(rightConditional.ToString());
            return sb.ToString();
        }
    }
}