using System;
using System.Runtime.Serialization;
using MyInterpreter.DataSource;

namespace MyInterpreter.Exceptions.ParserExceptions
{
    public class UnexpectedToken : InterpreterException
    {
        public UnexpectedToken() { } 
        public UnexpectedToken(string message, Exception inner) 
            : base(message, inner) { }
        public UnexpectedToken(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
        public UnexpectedToken(TextPosition position, string source) 
            : base(position, "Unexpected token", source) { }
    }
}