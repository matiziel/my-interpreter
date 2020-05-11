using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.SemanticAnalyzer;

namespace MyInterpreter.Parser.Ast.Statements
{
    public class IfStatement : Statement
    {
        private Conditional conditional;
        private Statement statementIf;
        private Statement statementElse;
        public IfStatement(Conditional conditional, Statement statementIf, Statement statementElse = null)
        {
            this.statementIf = statementIf;
            this.statementElse = statementElse;
            this.conditional = conditional;
        }
        public void Execute(ExecEnvironment environment)
        {
            throw new System.NotImplementedException();
        }
    }
}