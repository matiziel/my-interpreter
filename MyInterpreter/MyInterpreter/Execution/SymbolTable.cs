using System.Collections.Generic;
using MyInterpreter.Parser.Ast;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Execution {
    public class SymbolTable {
        private readonly Stack<FunctionCallScope> functionCallScopes;
        private readonly Scope globalScope;
        private Value returnedValue;
        public SymbolTable() {
            functionCallScopes = new Stack<FunctionCallScope>();
            globalScope = new Scope();
        }
        public Value ReturnedValue {
            get {
                var ret = returnedValue;
                returnedValue = null;
                return ret;
            }
        }
        public void OnFunctionCall(Variable returnedVariable) {
            functionCallScopes.Peek().RegisterVariable(returnedVariable);
            functionCallScopes.Push(new FunctionCallScope());
        }
        public void OnReturnFromFunction(Value returned) {
            functionCallScopes.Pop();
            this.returnedValue = returned;
        }
        public void RegisterVariable(Variable variable) {
            if (functionCallScopes.Count > 0)
                functionCallScopes.Peek().RegisterVariable(variable);
            else
                globalScope.RegisterVariable(variable);
        }
        public Variable GetVariable(string name) {
            Variable variable;
            if ((variable = functionCallScopes.Peek().GetVariable(name)) != null)
                return variable;
            else
                return globalScope.GetVariable(name);
        }

    }
}