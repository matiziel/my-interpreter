using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.Execution;

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
            for (first.Execute(environment); conditional.Evaluate(environment); second.Execute(environment))
                statement.Execute(environment);
        }
        public void Accept(PrintVisitor visitor) {
            visitor.VisitStatement("for");
            first.Accept(visitor);
            conditional.Accept(visitor);
            second.Accept(visitor);
            statement.Accept(visitor);
        }
    }
}