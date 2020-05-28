using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Execution;
using System.Text;
using MyInterpreter.Parser.Ast.Values;

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
            //else if (variable.Type == TypeValue.Matrix)
            //TODO 
            //variable.Value = new Matrix_t(variable.First.Evaluate()., variable.Second.Evaluate());
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