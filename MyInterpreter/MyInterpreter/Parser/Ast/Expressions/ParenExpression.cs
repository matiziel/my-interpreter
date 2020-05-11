using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public class ParenExpression : PrimaryExpression
    {
        Expression expression;
        public ParenExpression(Expression expression)  =>
            this.expression = expression;
        public Value Evaluate(ExecEnvironment environment) => expression.Evaluate(environment);
    }
}