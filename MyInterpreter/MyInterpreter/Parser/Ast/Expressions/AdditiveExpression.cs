namespace MyInterpreter.Parser.Ast.Expressions
{
    public class AdditiveExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        public object Evaluate()
        {
            throw new System.NotImplementedException();
        }
    }
}