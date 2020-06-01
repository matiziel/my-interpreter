using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;
using System.Text;
using MyInterpreter.Exceptions.ExecutionException;

namespace MyInterpreter.Parser.Ast {
    public class DerefVariable : PrimaryExpression {
        private string name;
        public Range Left { get; private set; }
        public Range Right { get; private set; }
        public DerefVariable(string name, Range left = null, Range right = null) {
            this.name = name;
            this.Left = left;
            this.Right = right;
        }
        public Value Evaluate(ExecEnvironment environment) {
            var variable = GetVariable(environment);
            if(Left is null || Right is null)
                return variable.Value;
            else if(Left != null && Right != null){
                return ExpressionEvaluator.EvaluateMatrixDerefVar(
                    Left.FirstExpr.Evaluate(environment),
                    Left.SecondExpr.Evaluate(environment),
                    Right.FirstExpr.Evaluate(environment),
                    Right.SecondExpr.Evaluate(environment),
                    variable
                );
            }
            else
                throw new RuntimeException("Wrong deref of variable");
        }
        public Variable GetVariable(ExecEnvironment environment) =>
            environment.GetVariable(name);
        
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(name);
            if (Left != null && Right != null) {
                sb.Append('[');
                sb.Append(Left.ToString());
                sb.Append(',');
                sb.Append(Right.ToString());
                sb.Append(']');
            }
            return sb.ToString();
        }

    }
}