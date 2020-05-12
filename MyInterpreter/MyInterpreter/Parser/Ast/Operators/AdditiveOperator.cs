using MyInterpreter.Exceptions.ParserExceptions;

namespace MyInterpreter.Parser.Ast.Operators
{
    public class AdditiveOperator : IOperator
    {
        public string Operator { get; private set; }
        public AdditiveOperator(string value)
        {
            if(value == "+" || value == "-")
                Operator = value;
        }
    }
}