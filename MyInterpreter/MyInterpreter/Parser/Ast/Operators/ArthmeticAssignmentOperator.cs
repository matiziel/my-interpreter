using System.Collections.Generic;
using MyInterpreter.Exceptions.ParserExceptions;

namespace MyInterpreter.Parser.Ast.Operators
{
    public class ArthmeticAssignmentOperator : IOperator
    {
        public string Operator { get; private set; }
        public ArthmeticAssignmentOperator(string value)
        {
            if(operators.Contains(value))
                Operator = value;
            else throw new WrongOperator();
        }
        private static HashSet<string> operators = new HashSet<string>() {
            "+=", "-=", "/=", "*=", "%="
        };
    }
}