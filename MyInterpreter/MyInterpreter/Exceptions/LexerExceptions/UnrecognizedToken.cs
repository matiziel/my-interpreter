using System;
using System.Runtime.Serialization;
using System.Text;
using MyInterpreter.DataSource;

namespace MyInterpreter.Exceptions.LexerExceptions
{
    [System.Serializable]
    public class UnrecognizedToken : InterpreterException
    {
        public UnrecognizedToken() { }
        public UnrecognizedToken(TextPosition position, string source) 
            : base(position, "Unrecognized token", source) { }
        public UnrecognizedToken(string message, Exception inner) 
            : base(message, inner) { }
        protected UnrecognizedToken(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}