using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;

namespace MyInterpreter.Parser.Ast.Conditionals
{
    public class Logical : Conditional
    {
        private Expression leftExpression;
        private Expression rightExpression;
        private EqualityOperator equalityOperator;
        private bool isNegated;
        public Logical(Expression left, Expression right, EqualityOperator equalityOperator, bool isNegated = false)
        {
            leftExpression = left;
            rightExpression = right;
            this.equalityOperator = equalityOperator;
            this.isNegated = isNegated;
        }
        public bool Evaluate()
        {
            throw new System.NotImplementedException();
        }
    }
}