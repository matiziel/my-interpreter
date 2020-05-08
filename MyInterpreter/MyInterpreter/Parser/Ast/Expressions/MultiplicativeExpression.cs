using MyInterpreter.Parser.Ast.Operators;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public class MultiplicativeExpression : Expression
    {        
        private Expression leftExpression;
        private Expression rightExpression;
        private MultiplicativeOperator multiplicativeOperator;
        public MultiplicativeExpression(Expression left, Expression right, MultiplicativeOperator operatorValue)
        {
            leftExpression = left;
            rightExpression = right;
            multiplicativeOperator = operatorValue;
        }
        public MultiplicativeExpression(Expression left)
        {
            leftExpression = left;
            rightExpression = null;
            multiplicativeOperator = null;
        }
        public object Evaluate()
        {
            throw new System.NotImplementedException();
        }
    }
}