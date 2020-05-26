using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Statements {
    public class WhileStatement : Statement {
        private Conditional conditional;
        private Statement statement;
        public WhileStatement(Conditional conditional, Statement statement) {
            this.conditional = conditional;
            this.statement = statement;
        }
        public void Execute(ExecEnvironment environment) {
            while (conditional.Evaluate(environment))
                statement.Execute(environment);
        }
        public void Accept(PrintVisitor visitor) {
            visitor.VisitStatement("while");
        }
    }
}