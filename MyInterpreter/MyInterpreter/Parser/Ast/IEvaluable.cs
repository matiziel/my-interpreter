namespace MyInterpreter.Parser.Ast
{
    public interface IEvaluable <Value>
    {
        Value Evaluate();
    }
}