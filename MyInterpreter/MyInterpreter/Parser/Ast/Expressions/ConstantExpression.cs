using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public class ConstantExpression : PrimaryExpression
    {
        private Value value;
        public ConstantExpression(Value value)
            =>  this.value = value;
        public Value Evaluate()
        {
            throw new System.NotImplementedException();
        }
    }
}