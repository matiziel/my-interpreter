using System.Collections.Generic;
using MyInterpreter.Parser.Ast.Statements;
using MyInterpreter.Parser.Ast.Values;
using System;
using System.Text;
using System.Linq;
using MyInterpreter.Execution;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Exceptions.ExecutionException;

namespace MyInterpreter.Parser.Ast {
    public class Function {
        public TypeValue Type { get; protected set; }
        protected string name;
        protected IEnumerable<Parameter> parameters;
        protected BlockStatement blockStatement;
        public Function(TypeValue type, string name, IEnumerable<Parameter> parameters, BlockStatement blockStatement) {
            this.Type = type;
            this.name = name;
            this.parameters = parameters;
            this.blockStatement = blockStatement;
        }
        public virtual void Execute(ExecEnvironment environment, IEnumerable<Value> arguments) {
            environment.OnFunctionCall();
            RegisterParameters(environment, arguments);

            blockStatement.Execute(environment);

            if (environment.ReturnFlag && Type == TypeValue.Void) {
                throw new RuntimeException("Function must return result");
            }
            environment.OnReturnFromFunction();
        }
        protected void RegisterParameters(ExecEnvironment environment, IEnumerable<Value> arguments) {
            if (arguments == null || parameters == null)
                return;
            if (arguments.Count() != parameters.Count())
                throw new RuntimeException("Wrong number of parameters");

            var parametersList = parameters.Zip(arguments);
            var variables = new List<Variable>();
            foreach (var param in parametersList) {
                if (param.First.Type != param.Second.Type)
                    throw new RuntimeException("Cannot cast " + param.Second.Type + " to " + param.First.Type);
                var variable = new Variable(param.First.Type, param.First.Name);
                variable.Value = param.Second;
                variables.Add(variable);
            }
            foreach (var value in variables)
                environment.AddVariable(value);
        }

        public override string ToString() {
            var sb = new StringBuilder("Function->");
            sb.Append(Type.ToString());
            sb.Append("->");
            sb.Append(name);
            sb.Append("\n");
            int i = 0;
            sb.Append("Parameters->");
            foreach (var param in parameters) {
                sb.Append(param.ToString());
                if (++i < parameters.Count())
                    sb.Append(",");
            }
            sb.Append("\n");
            sb.Append(blockStatement.ToString());
            return sb.ToString();
        }
    }
}