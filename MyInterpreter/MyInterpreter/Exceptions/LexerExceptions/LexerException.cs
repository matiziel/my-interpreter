using System;
using System.Text;
using MyInterpreter.DataSource;

namespace MyInterpreter.Exceptions.LexerExceptions {
    [System.Serializable]
    public abstract class LexerException : Exception {
        public LexerException() { }
        public LexerException(TextPosition position, string message, string source) : base(CreateMessage(position, message, source)) { }
        private static string CreateMessage(TextPosition position, string message, string source) {
            var sb = new StringBuilder(message);
            sb.Append(" at line: "); sb.Append(position.Row);
            sb.Append(", column: "); sb.Append(position.Column);
            sb.Append("\nSource:\n"); sb.Append(source);
            sb.Append("\n");
            for (int i = 1; i < position.Column; ++i)
                sb.Append(" ");
            sb.Append("^");
            return sb.ToString();
        }
        public LexerException(string message, System.Exception inner) : base(message, inner) { }
        protected LexerException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


    }
}