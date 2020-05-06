using System;
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
        public UnrecognizedToken(string message, System.Exception inner) : base(message, inner) { }
        protected UnrecognizedToken(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}