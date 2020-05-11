using System;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Execution
{
    public static class ExpressionExecutor
    {
        public static Func<Expression, Expression, IOperator, Value> ExecuteExpression()
        {
            throw new NotImplementedException();
        }
    }
}