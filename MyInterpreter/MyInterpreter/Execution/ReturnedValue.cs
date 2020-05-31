using System;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Execution {
    public class ReturnedValue : Exception {
        public Value Value { get; private set; }
        public ReturnedValue(Value value)
            => Value = value;
    }
}