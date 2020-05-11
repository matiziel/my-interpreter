using MyInterpreter.SemanticAnalyzer;

namespace MyInterpreter.Parser.Ast.Conditionals
{
    public class OrConditional : Conditional
    {
        private Conditional leftConditional;
        private Conditional rightConditional;
        public OrConditional(Conditional left, Conditional right)
        {
            leftConditional = left;
            rightConditional = right;
        }
        public bool Evaluate(ExecEnvironment environment) 
            => rightConditional is null ? 
                leftConditional.Evaluate(environment) :
                leftConditional.Evaluate(environment) || rightConditional.Evaluate(environment);
    }
}