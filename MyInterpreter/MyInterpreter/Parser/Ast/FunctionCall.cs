using System.Collections.Generic;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Statements;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;
using System.Text;
using System.Linq;

namespace MyInterpreter.Parser.Ast {
    public class FunctionCall : PrimaryExpression, Statement {
        private string name;
        private readonly IEnumerable<Expression> arguments;
        public FunctionCall(string name, IEnumerable<Expression> arguments) {
            this.name = name;
            this.arguments = arguments;
        }
        public void Execute(ExecEnvironment environment) {
            this.Evaluate(environment);
        }
        public Value Evaluate(ExecEnvironment environment) {
            Function fun = environment.GetFunctionByName(name);
            fun.Execute(environment);
            return environment.GetReturnedValue();
        }
        public override string ToString() {
            var sb = new StringBuilder("FunctionCall->");
            sb.Append(name);
            sb.Append("\nArguments->");
            int i = 0;
            foreach (var arg in arguments) {
                sb.Append(arg.ToString());
                if (++i < arguments.Count())
                    sb.Append(",");
            }
            sb.Append("\n");
            return sb.ToString();
        }
    }
}