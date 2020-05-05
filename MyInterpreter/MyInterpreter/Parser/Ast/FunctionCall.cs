using System.Collections.Generic;

namespace MyInterpreter.Parser.Ast
{
    public class FunctionCall
    {
        public string Name { get; private set; }
        private readonly List<Expression> parametersList;
        public FunctionCall(string name)
        {
            Name = name;
            parametersList = new List<Expression>();
        }
        public void AddParameter(Expression e) => parametersList.Add(e);
    }
}