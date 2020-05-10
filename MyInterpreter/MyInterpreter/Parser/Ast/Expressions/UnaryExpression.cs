using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public class UnaryExpression : Expression
    {
        private bool isNegated;
        private PrimaryExpression expression;
        public UnaryExpression(PrimaryExpression expression, bool isNegated)
        {
            this.expression = expression;
            this.isNegated = isNegated; 
        }
        public Value Evaluate()
        {
            throw new System.NotImplementedException();
        }
    }
}