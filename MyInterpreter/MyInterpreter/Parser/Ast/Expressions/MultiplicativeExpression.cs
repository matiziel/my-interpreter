using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.SemanticAnalyzer;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public class MultiplicativeExpression : Expression
    {        
        private Expression leftExpression;
        private Expression rightExpression;
        private MultiplicativeOperator multiplicativeOperator;
        public MultiplicativeExpression(Expression left, Expression right, MultiplicativeOperator operatorValue)
        {
            leftExpression = left;
            rightExpression = right;
            multiplicativeOperator = operatorValue;
        }
        public MultiplicativeExpression(Expression left)
        {
            leftExpression = left;
            rightExpression = null;
            multiplicativeOperator = null;
        }
        public Value Evaluate(ExecEnvironment environment)
        {
            throw new System.NotImplementedException();
        }
    }
}