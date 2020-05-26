using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Statements {
    public class Definition : Statement {
        private Variable variable;
        private Expression expression;
        public Definition(Variable variable, Expression expression = null) {
            this.variable = variable;
            this.expression = expression;
        }
        public void Execute(ExecEnvironment environment) {
            environment.AddVariable(variable);
            if (!(expression is null))
                environment.GetVariable(variable.Name).Value = expression.Evaluate(environment);
        }
        public void Accept(PrintVisitor visitor) {
            visitor.VisitStatement("Definition");
            variable.Accept(visitor);
            expression?.Accept(visitor);
        }
    }
}