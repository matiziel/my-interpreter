using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Execution;
using System.Text;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Exceptions;

namespace MyInterpreter.Parser.Ast.Statements {
    public class Definition : Statement {
        private Variable variable;
        private Expression expression;
        public Definition(Variable variable, Expression expression = null) {
            this.variable = variable;
            this.expression = expression;
        }
        public void Execute(ExecEnvironment environment) {

            if (!(expression is null))
                variable.Value = expression.Evaluate(environment);
            else if (variable.Type == TypeValue.Matrix) {
                Int_t first = variable.First.Evaluate(environment) as Int_t;
                Int_t second = variable.Second.Evaluate(environment) as Int_t;
                if (first is null || second is null)
                    throw new RuntimeException();
                variable.Value = new Matrix_t(first.Value, second.Value);
            }
            environment.AddVariable(variable);
        }
        public override string ToString() {
            var sb = new StringBuilder("Definition->");
            sb.Append(variable.ToString());
            if (!(expression is null))
                sb.Append(expression.ToString());
            sb.Append("\n");
            return sb.ToString();
        }
    }
}