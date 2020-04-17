using System;
using System.Text;
using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Exceptions
{
    [System.Serializable]
    public class UnrecognizedToken : Exception
    {
        public UnrecognizedToken() { }
        public UnrecognizedToken(TextPosition position, string message) : base(CreateMessage(position, message)) { }
        private static string CreateMessage(TextPosition position, string message)
        {
            var sb = new StringBuilder("Unrecognized token at line: ");
            sb.Append(position.Row);
            sb.Append(", column: ");
            sb.Append(position.Column);
            sb.Append("\nSource:\n");
            sb.Append(message);
            return sb.ToString();
        }
        public UnrecognizedToken(string message, System.Exception inner) : base(message, inner) { }
        protected UnrecognizedToken(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}