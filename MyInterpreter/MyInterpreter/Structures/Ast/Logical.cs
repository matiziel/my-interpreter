using MyInterpreter.Structures.Tokens;

namespace MyInterpreter.Structures.Ast
{
    public class Logical
    {
        public Expression FirstExpr { get; private set; }
        public Expression SecondExpr { get; private set; }
        public Operator Operator { get; private set; }

    }
}