using System.Collections.Generic;
using MyInterpreter.Parser.Ast;
using System.Linq;

namespace MyInterpreter.Execution {
    public class Scope {
        private Scope parentScope;
        private IDictionary<string, Variable> variables;
        private HashSet<string> parentValueNames;
        private Scope(Scope parentScope) {
            this.parentScope = parentScope;
            variables = new Dictionary<string, Variable>();
        }
        private Scope(Scope parentScope, IDictionary<string, Variable> variables) {
            this.parentScope = parentScope;
            this.variables = variables;
            this.parentValueNames = variables.Keys.ToHashSet();
        }

        public static Scope CreateLocalScope(Scope parentScope) {
            return new Scope(parentScope, parentScope.variables);
        }
        public static Scope CreateFunctionScope(Scope parentScope) {
            return new Scope(parentScope);
        }
        public static Scope DestroyScope(Scope scope) {
            foreach (var item in scope.parentValueNames)
                scope.variables.Remove(item);
            return scope.parentScope;
        }
        public Variable GetVariable(string name)
            => variables.ContainsKey(name) ? variables[name] : null;

        public void AddVariable(Variable var)
            => variables.Add(var.Name, var);
    }
}