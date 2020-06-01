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
        protected TypeValue type;
        protected string name;
        protected IEnumerable<Parameter> parameters;
        protected BlockStatement blockStatement;
        public Function(TypeValue type, string name, IEnumerable<Parameter> parameters, BlockStatement blockStatement) {
            this.type = type;
            this.name = name;
            this.parameters = parameters;
            this.blockStatement = blockStatement;
        }
        public virtual void Execute(ExecEnvironment environment, IEnumerable<Expression> arguments) {
            try {
                environment.OnFunctionCall();
                RegisterParameters(environment, arguments);

                blockStatement.Execute(environment);

                environment.OnReturnFromFunction();
                if (type != TypeValue.Void)
                    throw new RuntimeException("Function must return result");
            }
            catch (ReturnedValue e) {
                if (type != e.Value.Type)
                    throw new RuntimeException("Wrong return type");
                environment.OnReturnFromFunction(e.Value);
            }
        }
        protected void RegisterParameters(ExecEnvironment environment, IEnumerable<Expression> arguments) {
            if (arguments == null || parameters == null)
                return;
            if (arguments.Count() != parameters.Count())
                throw new RuntimeException("Wrong number of parameters");

            var variables = new List<Variable>();
            var parametersList = parameters.ToList();
            var argumentsList = arguments.ToList();
            for (int i = 0; i < argumentsList.Count; ++i) {
                var value = argumentsList[i].Evaluate(environment);
                if (value.Type != parametersList[i].Type)
                    throw new RuntimeException("Cannot cast " + value.Type + " to " + parametersList[i].Type);
                var variable = new Variable(value.Type, parametersList[i].Name);
                variable.Value = value;
                variables.Add(variable);
            }
            foreach (var variable in variables) {
                environment.AddVariable(variable);
            }
        }

        public override string ToString() {
            var sb = new StringBuilder("Function->");
            sb.Append(type.ToString());
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