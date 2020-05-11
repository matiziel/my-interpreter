using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.SemanticAnalyzer;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public class ParenExpression : PrimaryExpression
    {
        Expression expression;
        public ParenExpression(Expression expression)  =>
            this.expression = expression;
        public Value Evaluate(ExecEnvironment environment)
        {
            throw new System.NotImplementedException();
        }
    }
}