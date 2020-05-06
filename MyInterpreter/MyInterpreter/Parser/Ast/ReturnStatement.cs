namespace MyInterpreter.Parser.Ast
{
    public class ReturnStatement : Statement
    {
        public Expression Value { get; private set; }
        public ReturnStatement(Expression value) => Value = value;
    }
}