using System.Collections.Generic;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Statements;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast
{
    public class FunctionCall : PrimaryExpression, Statement
    {
        private string name;
        private readonly IEnumerable<Expression> arguments;
        public FunctionCall(string name, IEnumerable<Expression> arguments)
        {
            this.name = name;
            this.arguments = arguments;
        }        
        public void Execute(ExecEnvironment environment)
        {
            throw new System.NotImplementedException();
        }
        public Value Evaluate(ExecEnvironment environment)
        {
            throw new System.NotImplementedException();
        }
    }
}