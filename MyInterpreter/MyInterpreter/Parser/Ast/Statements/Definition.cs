using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Execution;
using System.Text;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Exceptions.ExecutionException;

namespace MyInterpreter.Parser.Ast.Statements {
    public class Definition : Statement {
        private Variable variable;
        private Expression expression;
        public Definition(Variable variable, Expression expression = null) {
            this.variable = variable;
            this.expression = expression;
        }
        public void Execute(ExecEnvironment environment) {
            if (variable.Type == TypeValue.Matrix) {
                AssignMatrix(environment);
            }
            else if (!(expression is null))
                variable.Value = expression.Evaluate(environment);

            environment.AddVariable(variable);
        }

        private void AssignMatrix(ExecEnvironment environment) {
            Int_t first = variable.First.Evaluate(environment) as Int_t;
            Int_t second = variable.Second.Evaluate(environment) as Int_t;
            if (first is null || second is null)
                throw new RuntimeException("Cannot define matrix without size");
            if (!(expression is null)) {
                var matrix = expression.Evaluate(environment) as Matrix_t;
                if (matrix.Value.SizeX != first.Value || matrix.Value.SizeY != second.Value)
                    throw new RuntimeException("Sizes of matrices is various");
                variable.Value = matrix;
            }
            else
                variable.Value = new Matrix_t(first.Value, second.Value);
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