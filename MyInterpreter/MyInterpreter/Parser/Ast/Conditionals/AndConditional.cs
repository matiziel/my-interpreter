namespace MyInterpreter.Parser.Ast.Conditionals
{
    public class AndConditional : Conditional
    {
        private Conditional leftConditional;
        private Conditional rightConditional;
        public AndConditional(Conditional left, Conditional right = null)
        {
            leftConditional = left;
            rightConditional = right;
        }
        public bool Evaluate()
        {
            if(rightConditional != null)
                return leftConditional.Evaluate() && rightConditional.Evaluate();
            else
                return leftConditional.Evaluate();
        }
    }
}