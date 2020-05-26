using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.Execution;
using System.Text;

namespace MyInterpreter.Parser.Ast.Statements {
    public class IfStatement : Statement {
        private Conditional conditional;
        private Statement statementIf;
        private Statement statementElse;
        public IfStatement(Conditional conditional, Statement statementIf, Statement statementElse = null) {
            this.statementIf = statementIf;
            this.statementElse = statementElse;
            this.conditional = conditional;
        }
        public void Execute(ExecEnvironment environment) {
            if (conditional.Evaluate(environment))
                statementIf.Execute(environment);
            else
                statementElse.Execute(environment);
        }
        public override string ToString() {
            var sb = new StringBuilder("if->");
            sb.Append(conditional.ToString());
            sb.Append("\n");
            sb.Append(statementIf.ToString());
            sb.Append("else->\n");
            sb.Append(statementElse.ToString());
            return sb.ToString();
        }
    }
}