using MyInterpreter.Exceptions.ParserExceptions;

namespace MyInterpreter.Parser.Ast.Operators
{
    public class MultiplicativeOperator : IOperator
    {
        public string Operator { get; private set; }
        public MultiplicativeOperator(string value)
        {
            if(value == "*" || value == "/" || value == "%")
                Operator = value;
            else 
                throw new WrongOperator();
        }
    }
}