using MyInterpreter.Parser.Ast.Operators;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public class AdditiveExpression : Expression
    {
        private Expression leftExpression;
        private Expression rightExpression;
        private AdditiveOperator additiveOperator;
        public AdditiveExpression(Expression left, Expression right, AdditiveOperator operatorValue)
        {
            leftExpression = left;
            rightExpression = right;
            additiveOperator = operatorValue;
        }
        public AdditiveExpression(Expression left)
        {
            leftExpression = left;
            rightExpression = null;
            additiveOperator = null;
        }
        public object Evaluate()
        {
            //lambda 
            throw new System.NotImplementedException();
        }
    }
}