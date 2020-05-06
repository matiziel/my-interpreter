using MyInterpreter.Parser.Ast.Operators;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public class MultiplicativeExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }
        public MultiplicativeOperator Operator { get; private set; }
        public MultiplicativeExpression(Expression left, Expression right, MultiplicativeOperator operatorValue)
        {
            LeftExpression = left;
            RightExpression = right;
            Operator = operatorValue;
        }
        public MultiplicativeExpression(Expression left)
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