using System.Text;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Conditionals {
    public class ParenConditional : Logical {
        private Conditional parenConditional;
        private bool isNegated;
        public ParenConditional(Conditional parenConditional, bool isNegated = false) {
            this.parenConditional = parenConditional;
            this.isNegated = isNegated;
        }
        public bool Evaluate(ExecEnvironment environment)
            => isNegated ? !parenConditional.Evaluate(environment) : parenConditional.Evaluate(environment);

        public override string ToString() {
            var sb = new StringBuilder();
            if (isNegated)
                sb.Append('!');
            sb.Append('(');
            sb.Append(parenConditional.ToString());
            sb.Append(')');
            return sb.ToString();
        }
    }
}