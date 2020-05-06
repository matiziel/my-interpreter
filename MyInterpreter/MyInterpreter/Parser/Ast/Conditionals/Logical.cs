using MyInterpreter.Parser.Ast.Expressions;

namespace MyInterpreter.Parser.Ast.Conditionals
{
    public class Logical : Conditional
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }
        public Logical(Expression left, Expression right)
        {
            LeftExpression = left;
            RightExpression = right;
        }
        public bool Evaluate()
        {
            throw new System.NotImplementedException();
        }
    }
}