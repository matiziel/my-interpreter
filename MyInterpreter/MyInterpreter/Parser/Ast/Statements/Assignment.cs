using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Statements {
    public class Assignment : Statement {
        public string Name { get; private set; }
        private AssignmentOperator assignmentOperator;
        private Expression expression;
        public Assignment(string name, AssignmentOperator assignmentOperator, Expression expression) {
            this.Name = name;
            this.assignmentOperator = assignmentOperator;
            this.expression = expression;
        }
        public void Execute(ExecEnvironment environment) {
            Variable var = environment.GetVariable(Name);
            //TODO can be different operators
            var.Value = expression.Evaluate(environment);
        }
        public void Accept(PrintVisitor visitor) {
            visitor.VisitAssignment(this);
            assignmentOperator.Accept(visitor);
            expression.Accept(visitor);
        }
    }
}