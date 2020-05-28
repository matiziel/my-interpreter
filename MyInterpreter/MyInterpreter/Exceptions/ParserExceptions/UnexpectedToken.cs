using System;
using System.Runtime.Serialization;
using MyInterpreter.DataSource;

namespace MyInterpreter.Exceptions.ParserExceptions {
    public class UnexpectedToken : Exception {
        public TextPosition Position { get; private set; }
        public UnexpectedToken() { }
        public UnexpectedToken(string message, Exception inner)
            : base(message, inner) { }
        public UnexpectedToken(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        public UnexpectedToken(TextPosition position)
            : base("Unexpected token") => Position = position;
    }
}