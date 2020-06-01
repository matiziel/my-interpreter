using System;
using System.Runtime.Serialization;

namespace MyInterpreter.Exceptions.ExecutionException
{
    public class ArthmeticOperationException : ExecutionException {
        public ArthmeticOperationException() {
        }

        public ArthmeticOperationException(string message) : base("Arthmetic exception: " + message) {
        }

        public ArthmeticOperationException(string message, Exception inner) : base(message, inner) {
        }

        protected ArthmeticOperationException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}