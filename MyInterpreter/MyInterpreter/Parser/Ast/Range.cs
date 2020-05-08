using MyInterpreter.Parser.Ast.Expressions;

namespace MyInterpreter.Parser.Ast
{
    public class Range
    {
        private Expression firstExpr;
        private Expression secondExpr;
        public Range(Expression first, Expression second)
        {
            firstExpr = first;
            secondExpr = second;
        }
    }
}