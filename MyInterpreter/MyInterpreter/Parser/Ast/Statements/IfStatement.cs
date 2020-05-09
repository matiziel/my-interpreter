using MyInterpreter.Parser.Ast.Conditionals;

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
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}