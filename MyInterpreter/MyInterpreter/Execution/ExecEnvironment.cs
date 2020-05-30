using System;
using System.Collections.Generic;
using MyInterpreter.Parser.Ast;

namespace MyInterpreter.Execution {
    public class ExecEnvironment {
        private readonly IDictionary<string, Function> functions;
        private readonly SymbolTable symbolTable;
        public ExecEnvironment(IDictionary<string, Function> functions) {
            this.functions = functions;
            symbolTable = new SymbolTable();
        }
        public void MakeLocalScope() =>
            symbolTable.MakeNewLocalScope();

        public void DestroyScope() =>
            symbolTable.DestroyLocalScope();

        public Variable GetVariable(string name) =>
            symbolTable.GetVariable(name);

        public void AddVariable(Variable var) =>
            symbolTable.RegisterVariable(var);
    }
}