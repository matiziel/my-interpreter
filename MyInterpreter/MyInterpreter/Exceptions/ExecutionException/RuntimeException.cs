using System;

namespace MyInterpreter.Exceptions.ExecutionException {
    public class RuntimeException : ExecutionException {
        public RuntimeException() { }
        public RuntimeException(string message) : base("Runtime exception: " + message) { }
        public RuntimeException(string message, System.Exception inner) : base(message, inner) { }
        protected RuntimeException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}