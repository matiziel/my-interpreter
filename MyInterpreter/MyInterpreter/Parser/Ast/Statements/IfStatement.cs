using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.Execution;

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
        public void Accept(PrintVisitor visitor) {
            visitor.VisitStatement("if");
            conditional.Accept(visitor);
            statementIf.Accept(visitor);
            visitor.VisitStatement("else");
            statementElse.Accept(visitor);
        }
    }
}