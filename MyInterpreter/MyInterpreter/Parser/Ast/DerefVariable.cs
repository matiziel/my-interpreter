using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast
{
    public class DerefVariable : PrimaryExpression
    {
        private string name;
        private Range left;
        private Range right;
        public DerefVariable(string name, Range left, Range right)
        {
            this.name = name;
            this.left = left;
            this.right = right;
        }
        public Value Evaluate()
        {
            throw new System.NotImplementedException();
        }
    }
}