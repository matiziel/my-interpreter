using System;
namespace MyInterpreter.Exceptions
{
    [System.Serializable]
    public class ExecutionException : System.Exception
    {
        public ExecutionException() { }
        public ExecutionException(string message) : base(message) { }
        public ExecutionException(string message, System.Exception inner) : base(message, inner) { }
        protected ExecutionException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}