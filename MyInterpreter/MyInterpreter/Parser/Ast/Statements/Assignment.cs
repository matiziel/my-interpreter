using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Execution;
using System.Text;

namespace MyInterpreter.Parser.Ast.Statements {
    public class Assignment : Statement {
        private string name;
        private AssignmentOperator assignmentOperator;
        private Expression expression;
        public Assignment(string name, AssignmentOperator assignmentOperator, Expression expression) {
            this.name = name;
            this.assignmentOperator = assignmentOperator;
            this.expression = expression;
        }
        public void Execute(ExecEnvironment environment) {
            Variable var = environment.GetVariable(name);
            //TODO can be different operators
            var.Value = expression.Evaluate(environment);
        }
        public override string ToString() {
            var sb = new StringBuilder("Assignment->\n");
            sb.Append(name);
            sb.Append(assignmentOperator.ToString());
            sb.Append(expression.ToString());
            sb.Append("\n");
            return sb.ToString();
        }
    }
}