using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Execution;
using MyInterpreter.Parser.Ast.Values;
using System.Text;

namespace MyInterpreter.Parser.Ast.Conditionals {
    public class SimpleConditional : Logical {
        private Expression leftExpression;
        private Expression rightExpression;
        private EqualityOperator equalityOperator;
        public SimpleConditional(Expression left, Expression right, EqualityOperator equality) {
            leftExpression = left;
            rightExpression = right;
            equalityOperator = equality;
        }
        public bool Evaluate(ExecEnvironment environment) {
            Value left = leftExpression.Evaluate(environment);
            Value right = rightExpression.Evaluate(environment);
            return ExpressionExecutor.EvaluateSimpleConditional(left, right, equalityOperator);
        }
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(leftExpression.ToString());
            sb.Append(equalityOperator.ToString());
            sb.Append(rightExpression.ToString());
            return sb.ToString();
        }
        
    }
}