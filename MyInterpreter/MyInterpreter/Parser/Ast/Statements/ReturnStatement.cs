using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Statements
{
    public class ReturnStatement : Statement
    {
        private Expression value;
        public ReturnStatement(Expression value) => this.value = value;

        public void Execute(ExecEnvironment environment)
        {
            throw new System.NotImplementedException();
        }
        public void Accept(PrintVisitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}