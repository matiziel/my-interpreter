using System;
using System.Collections.Generic;
using MyInterpreter.Parser.Ast;

namespace MyInterpreter.Execution {
    public class Scope {
        private readonly Dictionary<string, Variable> variables;
        public Scope() =>
            variables = new Dictionary<string, Variable>();

        public Variable GetVariable(string name) =>
            variables.ContainsKey(name) ? variables[name] : null;

        public bool RegisterVariable(Variable var) {
            if (variables.ContainsKey(var.Name))
                return false;
            variables.Add(var.Name, var);
            return true;
            
        }

    }
}