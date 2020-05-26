using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Execution;
using System.Text;

namespace MyInterpreter.Parser.Ast.Statements {
    public class Definition : Statement {
        private Variable variable;
        private Expression expression;
        public Definition(Variable variable, Expression expression = null) {
            this.variable = variable;
            this.expression = expression;
        }
        public void Execute(ExecEnvironment environment) {
            environment.AddVariable(variable);
            if (!(expression is null))
                environment.GetVariable(variable.Name).Value = expression.Evaluate(environment);
        }
        public override string ToString() {
            var sb = new StringBuilder("Definition->");
            sb.Append(variable.ToString());
            if(!(expression is null))
                sb.Append(expression.ToString());
            sb.Append("\n");
            return sb.ToString();
        }
    }
}