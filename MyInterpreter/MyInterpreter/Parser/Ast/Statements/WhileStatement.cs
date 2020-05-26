using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.Execution;
using System.Text;

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
        public override string ToString() {
            var sb = new StringBuilder("while->");
            sb.Append(conditional.ToString());
            sb.Append("\n");
            sb.Append(statement.ToString());
            return sb.ToString();
        }
    }
}