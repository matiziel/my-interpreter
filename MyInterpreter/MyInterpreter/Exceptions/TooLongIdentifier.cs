using System;
using System.Text;
using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Exceptions
{
    [System.Serializable]
    public class TooLongIdentifier : InterpreterException
    {
        public TooLongIdentifier() { }
        public TooLongIdentifier(TextPosition position, string source, int length) 
            : base(new TextPosition(position, length - 1), "Too long identifier", source) { }
        public TooLongIdentifier(string message, System.Exception inner) : base(message, inner) { }
        protected TooLongIdentifier(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}