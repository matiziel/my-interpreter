using System;
using System.Runtime.Serialization;
using System.Text;
using MyInterpreter.DataSource;

namespace MyInterpreter.Exceptions.LexerExceptions
{
    [System.Serializable]
    public class TooLongIdentifier : LexerException
    {
        public TooLongIdentifier() { }
        public TooLongIdentifier(TextPosition position, string source, int length) 
            : base(new TextPosition(position, length - 1), "Too long identifier", source) { }
        public TooLongIdentifier(string message, System.Exception inner) 
            : base(message, inner) { }
        protected TooLongIdentifier(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}