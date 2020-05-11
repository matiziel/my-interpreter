using MyInterpreter.Parser.Ast.Expressions;

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
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}