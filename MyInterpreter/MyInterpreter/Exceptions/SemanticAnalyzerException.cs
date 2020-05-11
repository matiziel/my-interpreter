using System;
using System.Runtime.Serialization;
using MyInterpreter.DataSource;

namespace MyInterpreter.Exceptions
{
    public class SemanticAnalyzerException : InterpreterException
    {
        public SemanticAnalyzerException()
        {
        }

        public SemanticAnalyzerException(string message, Exception inner) : base(message, inner)
        {
        }

        public SemanticAnalyzerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SemanticAnalyzerException(TextPosition position, string message, string source) : base(position, message, source)
        {
        }
    }
}