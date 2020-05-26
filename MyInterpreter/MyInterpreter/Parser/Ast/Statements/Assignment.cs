using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Execution;
using System.Text;
using System;

namespace MyInterpreter.Parser.Ast.Statements {
    public class Assignment : Statement {
        private DerefVariable variable;
        private AssignmentOperator assignmentOperator;
        private Expression expression;
        public Assignment(DerefVariable variable, AssignmentOperator assignmentOperator, Expression expression) {
            this.variable = variable;
            this.assignmentOperator = assignmentOperator;
            this.expression = expression;
        }
        public void Execute(ExecEnvironment environment) {
            throw new NotImplementedException();
        }
        public override string ToString() {
            var sb = new StringBuilder("Assignment->");
            sb.Append(variable.ToString());
            sb.Append(assignmentOperator.ToString());
            sb.Append(expression.ToString());
            sb.Append("\n");
            return sb.ToString();
        }
    }
}