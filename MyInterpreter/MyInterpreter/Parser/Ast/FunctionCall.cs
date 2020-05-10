using System.Collections.Generic;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Statements;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast
{
    public class FunctionCall : Expression, Statement
    {
        private string name;
        private readonly IEnumerable<Expression> arguments;
        public FunctionCall(string name, IEnumerable<Expression> arguments)
        {
            this.name = name;
            this.arguments = arguments;
        }

        public Value Evaluate()
        {
            throw new System.NotImplementedException();
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}