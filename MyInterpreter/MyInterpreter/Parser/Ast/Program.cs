using System.Collections.Generic;
using System.Text;
using MyInterpreter.Exceptions.ExecutionException;
using MyInterpreter.Execution;
using MyInterpreter.Parser.Ast.Statements;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast {
    public class Program {
        private readonly IDictionary<string, Function> functions;
        private readonly IEnumerable<Definition> definitions;
        public Program(IDictionary<string, Function> functions, IEnumerable<Definition> definitions) {
            this.functions = functions;
            this.definitions = definitions;
        }
        public int Execute() {
            var environment = new ExecEnvironment(functions);
            foreach (var def in definitions)
                def.Execute(environment);
            Function main = environment.GetFunctionByName("main");
            if (main is null)
                throw new RuntimeException("Cannot find main function");
            main.Execute(environment, null);
            return (environment.GetReturnedValue() as Int_t).Value;

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