using MyInterpreter.Parser.Ast.Expressions;

namespace MyInterpreter.Parser.Ast.Statements
{
    public class ReturnStatement : Statement
    {
        public Expression Value { get; private set; }
        public ReturnStatement(Expression value) => Value = value;

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}