using System.Collections.Generic;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Statements;

namespace MyInterpreter.Parser.Ast
{
    public class FunctionCall : Expression, Statement
    {
        private string name;
        private readonly IEnumerable<Expression> parameters;
        public FunctionCall(string name, IEnumerable<Expression> parameters)
        {
            this.name = name;
            this.parameters = parameters;
        }

        public object Evaluate()
        {
            throw new System.NotImplementedException();
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}