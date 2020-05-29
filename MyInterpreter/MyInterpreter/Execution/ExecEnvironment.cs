using System;
using System.Collections.Generic;
using MyInterpreter.Parser.Ast;

namespace MyInterpreter.Execution {
    public class ExecEnvironment {
        private readonly IDictionary<string, Function> _functions;

        public ExecEnvironment(IDictionary<string, Function> functions) {
            throw new NotImplementedException();
        }
        public void MakeLocalScope() {
            throw new NotImplementedException();
        }
        public void MakeFunctionScope() {
            throw new NotImplementedException();
        }
        public void DestroyScope()
            => throw new NotImplementedException();

        public Variable GetVariable(string name)
            => throw new NotImplementedException();

        public void AddVariable(Variable var)
            => throw new NotImplementedException();
    }
}