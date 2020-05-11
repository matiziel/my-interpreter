using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.SemanticAnalyzer;

namespace MyInterpreter.Parser.Ast.Statements
{
    public class WhileStatement : Statement
    {
        private Conditional conditional;
        private Statement statement;
        public WhileStatement(Conditional conditional, Statement statement)
        {
            this.conditional = conditional;
            this.statement = statement;
        }
        public void Execute(ExecEnvironment environment)
        {
            throw new System.NotImplementedException();
        }
    }
}