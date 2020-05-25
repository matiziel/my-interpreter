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
            => rightConditional is null ?
                leftConditional.Evaluate(environment) :
                leftConditional.Evaluate(environment) || rightConditional.Evaluate(environment);
        public void Accept(PrintVisitor visitor) {
            leftConditional.Accept(visitor);
            visitor.VisitOrConditional(this);
            rightConditional.Accept(visitor);
        }
    }
}