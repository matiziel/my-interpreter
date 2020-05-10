using MyInterpreter.Parser.Ast.Expressions;

namespace MyInterpreter.Parser.Ast.Statements
{
    public class Definition : Statement
    {
        private string type;
        private string name;
        private Expression expression;
        public Definition(string type, string name, Expression expression = null)
        {
            this.type = type;
            this.name = name;
            this.expression = expression;
        }
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}