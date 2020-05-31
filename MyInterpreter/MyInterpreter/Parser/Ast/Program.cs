using System.Collections.Generic;
using System.Text;
using MyInterpreter.Exceptions;
using MyInterpreter.Execution;
using MyInterpreter.Parser.Ast.Statements;

namespace MyInterpreter.Parser.Ast {
    public class Program {
        private readonly IDictionary<string, Function> functions;
        private readonly IEnumerable<Definition> definitions;
        public Program(IDictionary<string, Function> functions, IEnumerable<Definition> definitions) {
            this.functions = functions;
            this.definitions = definitions;
        }
        public void Execute() {
            var environment = new ExecEnvironment(functions);
            foreach (var def in definitions)
                def.Execute(environment);
            Function main = environment.GetFunctionByName("main");
            if (main is null)
                throw new RuntimeException();
            main.Execute(environment);
        }

        public override string ToString() {
            var sb = new StringBuilder("Program->\n");
            foreach (var def in definitions) 
                sb.Append(def.ToString());
            foreach (var fun in functions) 
                sb.Append(fun.Value.ToString());
            return sb.ToString();
        }
    }
}