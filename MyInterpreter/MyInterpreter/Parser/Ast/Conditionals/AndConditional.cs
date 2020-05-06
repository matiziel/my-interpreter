namespace MyInterpreter.Parser.Ast.Conditionals
{
    public class AndConditional : Conditional
    {
        public Conditional LeftConditional { get; private set; }
        public Conditional RightConditional { get; private set; }
        public AndConditional(Conditional left, Conditional right = null)
        {
            LeftConditional = left;
            RightConditional = right;
        }
        public bool Evaluate()
        {
            if(RightConditional != null)
                return LeftConditional.Evaluate() && RightConditional.Evaluate();
            else
                return LeftConditional.Evaluate();
        }
    }
}