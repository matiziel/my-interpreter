using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;
using System.Text;

namespace MyInterpreter.Parser.Ast.Expressions {
    public class MultiplicativeExpression : Expression {
        private Expression leftExpression;
        private Expression rightExpression;
        private MultiplicativeOperator multiplicativeOperator;
        public MultiplicativeExpression(Expression left, Expression right, MultiplicativeOperator operatorValue) {
            leftExpression = left;
            rightExpression = right;
            multiplicativeOperator = operatorValue;
        }
        public MultiplicativeExpression(Expression left) {
            leftExpression = left;
            rightExpression = null;
            multiplicativeOperator = null;
        }
        public Value Evaluate(ExecEnvironment environment) {
            return ExpressionEvaluator.Evaluate(
                leftExpression.Evaluate(environment),
                rightExpression.Evaluate(environment),
                multiplicativeOperator
            );
        }
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(leftExpression.ToString());
            sb.Append(multiplicativeOperator.ToString());
            sb.Append(rightExpression.ToString());
            return sb.ToString();
        }
    }
}