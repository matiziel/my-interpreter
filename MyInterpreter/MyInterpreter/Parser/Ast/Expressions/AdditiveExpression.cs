using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;
using System.Text;

namespace MyInterpreter.Parser.Ast.Expressions {
    public class AdditiveExpression : Expression {
        private Expression leftExpression;
        private Expression rightExpression;
        private AdditiveOperator additiveOperator;
        public AdditiveExpression(Expression left, Expression right, AdditiveOperator operatorValue) {
            leftExpression = left;
            rightExpression = right;
            additiveOperator = operatorValue;
        }
        public AdditiveExpression(Expression left) {
            leftExpression = left;
            rightExpression = null;
            additiveOperator = null;
        }
        public Value Evaluate(ExecEnvironment environment) {
            return ExpressionExecutor.EvaluateExpression(
                leftExpression.Evaluate(environment),
                rightExpression.Evaluate(environment),
                additiveOperator
            );
        }
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(leftExpression.ToString());
            sb.Append(additiveOperator.ToString());
            sb.Append(rightExpression.ToString());
            return sb.ToString();
        }
    }
}