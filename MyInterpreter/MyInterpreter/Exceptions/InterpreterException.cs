using System;
using System.Text;
using MyInterpreter.DataSource;

namespace MyInterpreter.Exceptions
{
    [System.Serializable]
    public abstract class InterpreterException : Exception
    {
        public InterpreterException() { }
        public InterpreterException(TextPosition position, string message, string source) : base(CreateMessage(position, message, source)) { }
        private static string CreateMessage(TextPosition position, string message, string source)
        {
            var sb = new StringBuilder(message);
            sb.Append(" at line: "); sb.Append(position.Row);
            sb.Append(", column: "); sb.Append(position.Column);
            sb.Append("\nSource:\n"); sb.Append(source);
            sb.Append("\n");
            for(int i = 1; i < position.Column; ++i)
                sb.Append(" ");
            sb.Append("^");
            return sb.ToString();
        }
        public InterpreterException(string message, System.Exception inner) : base(message, inner) { }
        protected InterpreterException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


    }
}