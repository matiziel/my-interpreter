using System;
using System.Runtime.Serialization;

namespace MyInterpreter.Exceptions.ExecutionException
{
    public class EnvironmentException : ExecutionException {
        public EnvironmentException() {
        }

        public EnvironmentException(string message) : base("Environment exception: " + message) {
        }

        public EnvironmentException(string message, Exception inner) : base(message, inner) {
        }

        protected EnvironmentException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}