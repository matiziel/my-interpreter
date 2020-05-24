using System.Collections.Generic;
using MyInterpreter.Exceptions.ParserExceptions;

namespace MyInterpreter.Parser.Ast.Operators
{
    public class AssignmentOperator : IOperator
    {
        public string Operator { get; private set; }
        public AssignmentOperator(string value) => Operator = value;
        public void Accept(PrintVisitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}