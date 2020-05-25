using System.Collections.Generic;
using MyInterpreter.Exceptions.ParserExceptions;

namespace MyInterpreter.Parser.Ast.Operators {
    public class EqualityOperator : IOperator {
        public string Operator { get; private set; }
        public EqualityOperator(string value) => Operator = value;
        public void Accept(PrintVisitor visitor) {
            visitor.VisitOperator(this);
        }

    }
}