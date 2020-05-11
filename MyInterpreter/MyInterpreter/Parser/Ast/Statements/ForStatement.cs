using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.SemanticAnalyzer;

namespace MyInterpreter.Parser.Ast.Statements
{
    public class ForStatement : Statement
    {
        private Assignment first;
        private Assignment second;
        private Conditional conditional;
        private Statement statement;
        public ForStatement(Statement statement, Conditional conditional, Assignment first, Assignment second)
        {
            this.statement = statement;
            this.conditional = conditional;
            this.first = first;
            this.second = second;
        }
        public void Execute(ExecEnvironment environment)
        {
            throw new System.NotImplementedException();
        }
    }
}