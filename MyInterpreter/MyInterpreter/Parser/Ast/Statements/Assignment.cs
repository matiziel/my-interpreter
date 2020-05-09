using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;

namespace MyInterpreter.Parser.Ast.Statements
{
    public class Assignment : Statement
    {
        private string name;
        private AssignmentOperator assignmentOperator;
        private Expression expression;
        public Assignment(string name, AssignmentOperator assignmentOperator, Expression expression)
        {
            this.name = name;
            this.assignmentOperator = assignmentOperator;
            this.expression = expression;
        }
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}