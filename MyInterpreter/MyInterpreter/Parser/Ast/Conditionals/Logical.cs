using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;

namespace MyInterpreter.Parser.Ast.Conditionals
{
    public class Logical : Conditional
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }
        public EqualityOperator Operator { get; private set; }
        public bool IsNegated { get; private set; }
        public Logical(Expression left, Expression right, bool isNegated = false)
        {
            LeftExpression = left;
            RightExpression = right;
            IsNegated = isNegated;
        }
        public bool Evaluate()
        {
            throw new System.NotImplementedException();
        }
    }
}