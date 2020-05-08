using MyInterpreter.Parser.Ast.Expressions;

namespace MyInterpreter.Parser.Ast.Statements
{
    public class ReturnStatement : Statement
    {
        private Expression value;
        public ReturnStatement(Expression value) => this.value = value;

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}