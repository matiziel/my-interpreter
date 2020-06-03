using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.Execution;
using System.Text;

namespace MyInterpreter.Parser.Ast.Statements {
    public class ForStatement : Statement {
        private Assignment first;
        private Assignment second;
        private Conditional conditional;
        private Statement statement;
        public ForStatement(Statement statement, Conditional conditional, Assignment first, Assignment second) {
            this.statement = statement;
            this.conditional = conditional;
            this.first = first;
            this.second = second;
        }
        public void Execute(ExecEnvironment environment) {
            for (first.Execute(environment); conditional.Evaluate(environment); second.Execute(environment)){
                if(environment.ReturnFlag)
                    break;
                statement.Execute(environment);
            }
        }
        public override string ToString() {
            var sb = new StringBuilder("for->\n");
            sb.Append("first->\n");
            sb.Append(first.ToString());
            sb.Append("conditional->");
            sb.Append(conditional.ToString());
            sb.Append("\nsecond->\n");
            sb.Append(second.ToString());
            sb.Append(statement.ToString());
            return sb.ToString();
        }
    }
}