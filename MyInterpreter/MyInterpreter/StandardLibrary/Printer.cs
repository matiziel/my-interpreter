using System.Collections.Generic;
using System.Linq;
using MyInterpreter.Exceptions.ExecutionException;
using MyInterpreter.Execution;
using MyInterpreter.Parser.Ast;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Statements;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.StandardLibrary {
    public class Printer : Function {
        public Printer()
            : base(TypeValue.Void, "print", null, null) {
        }
        public override void Execute(ExecEnvironment environment, IEnumerable<Value> arguments) {
            if (arguments == null || arguments.Count() <= 0)
                throw new RuntimeException("Function: print requires one argument");
            foreach (var expr in arguments)
                System.Console.WriteLine(expr);
        }
    }
}