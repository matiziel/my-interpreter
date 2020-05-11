using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.SemanticAnalyzer;

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
        public void Execute(ExecEnvironment environment)
        {
            throw new System.NotImplementedException();
        }
    }
}