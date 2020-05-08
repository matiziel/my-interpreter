using System.Collections.Generic;
using MyInterpreter.Parser.Ast.Statements;

namespace MyInterpreter.Parser.Ast
{
    public class Program
    {
        private readonly IDictionary<string, Function> functions;
        private readonly IEnumerable<Definition> definitions; 
        public Program(IDictionary<string, Function> functions, IEnumerable<Definition> definitions)
        {
            this.functions = functions;
            this.definitions = definitions;
        }
        public Function GetFunctionByName(string name) 
            => functions.ContainsKey(name) ? functions[name] : null;
    }
}