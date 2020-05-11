using MyInterpreter.SemanticAnalyzer;

namespace MyInterpreter.Parser.Ast.Conditionals
{
    public class ParenConditional : Logical
    {
        private Conditional parenConditional;
        public ParenConditional(Conditional parenConditional, bool isNegated = false)
        {
            this.parenConditional = parenConditional;
            this.isNegated = isNegated;
        }
        public override bool Evaluate(ExecEnvironment environment)
            => isNegated ? !parenConditional.Evaluate(environment) : parenConditional.Evaluate(environment);
    }
}