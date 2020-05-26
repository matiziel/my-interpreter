using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;
using System.Text;

namespace MyInterpreter.Parser.Ast.Expressions {
    public class ParenExpression : PrimaryExpression {
        Expression expression;
        public ParenExpression(Expression expression) =>
            this.expression = expression;

        public Value Evaluate(ExecEnvironment environment) => expression.Evaluate(environment);
       
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append('(');
            sb.Append(expression.ToString());
            sb.Append(')');
            return sb.ToString();
        }
    }
}