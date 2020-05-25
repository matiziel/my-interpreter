using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Execution;

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
            var.Value = expression.Evaluate(environment);
        }
        public void Accept(PrintVisitor visitor) {
            throw new System.NotImplementedException();
        }
    }
}