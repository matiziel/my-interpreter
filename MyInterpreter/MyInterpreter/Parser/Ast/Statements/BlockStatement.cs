using System.Collections.Generic;
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
        public void Accept(PrintVisitor visitor) {
            visitor.VisitStatement("BlockStatement");
            foreach (var item in statements) {
                item.Accept(visitor);
            }
        }
    }
}