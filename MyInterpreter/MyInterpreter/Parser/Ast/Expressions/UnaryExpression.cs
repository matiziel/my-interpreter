using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Expressions {
    public class UnaryExpression : Expression {
        private bool isNegated;
        private PrimaryExpression expression;
        public UnaryExpression(PrimaryExpression expression, bool isNegated) {
            this.expression = expression;
            this.isNegated = isNegated;
        }
        public Value Evaluate(ExecEnvironment environment)
            => isNegated ?
                ExpressionExecutor.GetNegative(expression.Evaluate(environment))
                : expression.Evaluate(environment);
        public void Accept(PrintVisitor visitor) {
            if(isNegated)
                visitor.VisitNegatedExpression(this);
            expression.Accept(visitor);
        }
    }
}