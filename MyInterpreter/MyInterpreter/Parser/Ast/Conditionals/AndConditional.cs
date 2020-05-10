namespace MyInterpreter.Parser.Ast.Conditionals
{
    public class AndConditional : Conditional
    {
        private Conditional leftConditional;
        private Conditional rightConditional;
        public AndConditional(Conditional left, Conditional right)
        {
            leftConditional = left;
            rightConditional = right;
        }
        public bool Evaluate() 
            => leftConditional.Evaluate() && rightConditional.Evaluate();
    }
}