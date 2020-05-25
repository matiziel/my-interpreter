using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Conditionals {
    public class ParenConditional : Logical {
        private Conditional parenConditional;
        public ParenConditional(Conditional parenConditional, bool isNegated = false) {
            this.parenConditional = parenConditional;
            this.isNegated = isNegated;
        }
        public override bool Evaluate(ExecEnvironment environment)
            => isNegated ? !parenConditional.Evaluate(environment) : parenConditional.Evaluate(environment);
        public override void Accept(PrintVisitor visitor) {
            visitor.VisitParenConditional(this, '(');
            parenConditional.Accept(visitor);
            visitor.VisitParenConditional(this, ')');
        }
    }
}