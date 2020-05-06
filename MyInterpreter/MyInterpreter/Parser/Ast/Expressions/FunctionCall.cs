using System.Collections.Generic;
using MyInterpreter.Parser.Ast.Expressions;

namespace MyInterpreter.Parser.Ast
{
    public class FunctionCall : Expression
    {
        public string Name { get; private set; }
        private readonly List<Expression> parametersList;
        public FunctionCall(string name)
        {
            Name = name;
            parametersList = new List<Expression>();
        }
        public void AddParameter(Expression e) => parametersList.Add(e);

        public object Evaluate()
        {
            throw new System.NotImplementedException();
        }
    }
}