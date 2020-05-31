using System;
using System.Collections.Generic;
using MyInterpreter.Parser.Ast;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.StandardLibrary;

namespace MyInterpreter.Execution {
    public class ExecEnvironment {
        private readonly IDictionary<string, Function> functions;
        private readonly SymbolTable symbolTable;
        public ExecEnvironment(IDictionary<string, Function> functions) {
            this.functions = functions;
            functions.Add("print", new Printer());
            symbolTable = new SymbolTable();
        }
        public Function GetFunctionByName(string name)
            => functions.ContainsKey(name) ? functions[name] : null;

        public void MakeLocalScope() =>
            symbolTable.MakeNewLocalScope();
        public void DestroyScope() =>
            symbolTable.DestroyLocalScope();

        public void OnFunctionCall() =>
            symbolTable.OnFunctionCall();

        public void OnReturnFromFunction(Value returned = null) =>
            symbolTable.OnReturnFromFunction(returned);

        public Value GetReturnedValue()
            => symbolTable.ReturnedValue;

        public Variable GetVariable(string name) =>
            symbolTable.GetVariable(name);

        public void AddVariable(Variable var) =>
            symbolTable.RegisterVariable(var);
    }
}