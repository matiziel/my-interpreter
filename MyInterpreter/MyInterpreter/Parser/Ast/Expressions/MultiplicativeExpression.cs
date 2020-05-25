using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;

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
            return ExpressionExecutor.EvaluateExpression(
                leftExpression.Evaluate(environment),
                rightExpression.Evaluate(environment),
                multiplicativeOperator
            );
        }
        public void Accept(PrintVisitor visitor) {
            throw new System.NotImplementedException();
        }
    }
}