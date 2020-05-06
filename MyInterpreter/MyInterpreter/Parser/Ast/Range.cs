using MyInterpreter.Parser.Ast.Expressions;

namespace MyInterpreter.Parser.Ast
{
    public class Range
    {
        public Expression FirstExpr { get; private set; }
        public Expression SecondExpr { get; private set; }
        public Range(Expression first, Expression second)
        {
            FirstExpr = first;
            SecondExpr = second;
        }
    }
}