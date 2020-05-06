using System;
using System.Runtime.Serialization;
using MyInterpreter.DataSource;

namespace MyInterpreter.Exceptions.ParserExceptions
{
    public class WrongOperator : InterpreterException
    {
        public WrongOperator() { }
        public WrongOperator(string message, Exception inner) : base(message, inner) { }
        public WrongOperator(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public WrongOperator(TextPosition position, string message, string source) : base(position, message, source) { }
    }
}