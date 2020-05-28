using System.Collections.Generic;
using MyInterpreter.Parser.Ast;

namespace MyInterpreter.Execution {
    public class ExecEnvironment {
        private readonly IDictionary<string, Function> _functions;
        private Scope currentScope;

        public ExecEnvironment(IDictionary<string, Function> functions) {
            _functions = functions;
            currentScope = null;
            MakeFunctionScope();
        }
        public void MakeLocalScope() {
            Scope localScope = Scope.CreateLocalScope(currentScope);
            currentScope = localScope;
        }
        public void MakeFunctionScope() {
            Scope localScope = Scope.CreateFunctionScope(currentScope);
            currentScope = localScope;
        }
        public void DestroyScope()
            => currentScope = Scope.DestroyScope(currentScope);

        public Variable GetVariable(string name)
            => currentScope.GetVariable(name);

        public void AddVariable(Variable var)
            => currentScope.AddVariable(var);
    }
}