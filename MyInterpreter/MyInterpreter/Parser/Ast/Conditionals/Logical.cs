namespace MyInterpreter.Parser.Ast.Conditionals
{
    public abstract class Logical : Conditional
    {
        protected bool isNegated;
        public abstract bool Evaluate();
    }
}