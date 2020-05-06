using MyInterpreter.Parser.Ast.Operators;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public class AdditiveExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }
        public AdditiveOperator Operator { get; private set; }
        public AdditiveExpression(Expression left, Expression right, AdditiveOperator operatorValue)
        {
            LeftExpression = left;
            RightExpression = right;
            Operator = operatorValue;
        }
        public AdditiveExpression(Expression left)
        {
            LeftExpression = left;
            RightExpression = null;
            Operator = null;
        }
        public object Evaluate()
        {
            throw new System.NotImplementedException();
        }
    }
}