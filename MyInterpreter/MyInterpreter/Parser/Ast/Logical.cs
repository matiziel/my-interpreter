using MyInterpreter.Tokens;

namespace MyInterpreter.Parser.Ast
{
    public class Logical
    {
        public Expression FirstExpr { get; private set; }
        public Expression SecondExpr { get; private set; }
        public Operator Operator { get; private set; }

    }
}