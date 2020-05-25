using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Execution;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast.Conditionals {
    public class SimpleConditional : Logical {
        private Expression leftExpression;
        private Expression rightExpression;
        private EqualityOperator equalityOperator;
        public SimpleConditional(Expression left, Expression right, EqualityOperator equality, bool isNegated) {
            leftExpression = left;
            rightExpression = right;
            equalityOperator = equality;
            this.isNegated = isNegated;
        }
        public override bool Evaluate(ExecEnvironment environment) {
            Value left = leftExpression.Evaluate(environment);
            Value right = rightExpression.Evaluate(environment);
            return ExpressionExecutor.EvaluateSimpleConditional(left, right, equalityOperator);
        }
        public override void Accept(PrintVisitor visitor) {
            throw new System.NotImplementedException();
        }
    }
}