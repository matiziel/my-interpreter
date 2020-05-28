using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;
using System.Text;

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
                ExpressionEvaluator.GetNegative(expression.Evaluate(environment))
                : expression.Evaluate(environment);
       
        public override string ToString() {
            var sb = new StringBuilder();
            if(isNegated)
                sb.Append("-");
            sb.Append(expression.ToString());
            return sb.ToString();
        }
    }
}