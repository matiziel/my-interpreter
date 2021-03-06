using System.Collections.Generic;
using MyInterpreter.Exceptions.ExecutionException;
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
        public void OnFunctionCall() {
            functionCallScopes.Push(new FunctionCallScope());
        }
        public void RegisterReturnValue(Value returned) {
            this.returnedValue = returned;
        }
        public void OnReturnFromFunction() {
            functionCallScopes.Pop();
        }
        public void MakeNewLocalScope() =>
            functionCallScopes.Peek().MakeNewLocalScope();

        public void DestroyLocalScope() =>
            functionCallScopes.Peek().DestroyLocalScope();

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
            else if ((variable = globalScope.GetVariable(name)) != null)
                return variable;
            else
                throw new EnvironmentException("Variable: " + name + " does not exists");
        }

    }
}