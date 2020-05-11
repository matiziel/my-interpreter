using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.SemanticAnalyzer;

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
        public Value Evaluate(ExecEnvironment environment)
        {
            throw new System.NotImplementedException();
        }
    }
}