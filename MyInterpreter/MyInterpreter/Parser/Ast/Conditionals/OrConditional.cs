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
        public bool Evaluate() 
            => leftConditional.Evaluate() || rightConditional.Evaluate();
    }
}