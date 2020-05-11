using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.SemanticAnalyzer;

namespace MyInterpreter.Parser.Ast.Statements
{
    public class Definition : Statement
    {
        private Variable variable;
        private Expression expression;
        public Definition(Variable variable, Expression expression = null)
        {
            this.variable = variable;
            this.expression = expression;
        }
        public void Execute(ExecEnvironment environment)
        {
            throw new System.NotImplementedException();
        }
    }
}