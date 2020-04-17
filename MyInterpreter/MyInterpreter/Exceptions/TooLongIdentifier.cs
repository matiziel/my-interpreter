using System;
using System.Text;
using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Exceptions
{
    [System.Serializable]
    public class TooLongIdentifier : Exception
    {
        public TooLongIdentifier() { }
        public TooLongIdentifier(TextPosition position, string message) : base(CreateMessage(position, message)) { }
        private static string CreateMessage(TextPosition position, string message)
        {
            var sb = new StringBuilder("Too long identifier at line: ");
            sb.Append(position.Row);
            sb.Append(", column: ");
            sb.Append(position.Column);
            sb.Append("\nSource:\n");
            sb.Append(message);
            return sb.ToString();
        }
        public TooLongIdentifier(string message, System.Exception inner) : base(message, inner) { }
        protected TooLongIdentifier(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}