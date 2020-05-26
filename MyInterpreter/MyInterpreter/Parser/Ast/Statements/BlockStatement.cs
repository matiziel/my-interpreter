using System.Collections.Generic;
using System.Text;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Statements {
    public class BlockStatement : Statement {
        private IEnumerable<Statement> statements;
        public BlockStatement(IEnumerable<Statement> statements)
            => this.statements = statements;
        public void Execute(ExecEnvironment environment) {
            environment.MakeLocalScope();
            foreach (var item in statements)
                item.Execute(environment);
            environment.DestroyScope();
        }
        public override string ToString() {
            var sb = new StringBuilder("BlockStatement->\n");
            foreach (var stat in statements) {
                sb.Append(stat.ToString());
            }
            return sb.ToString();
        }
    }
}