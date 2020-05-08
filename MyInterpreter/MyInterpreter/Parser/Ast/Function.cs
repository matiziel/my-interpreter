using System.Collections.Generic;
using MyInterpreter.Parser.Ast.Statements;

namespace MyInterpreter.Parser.Ast
{
    public class Function
    {
        public string Type { get; private set; }
        public string Name { get; private set; }
        private List<Parameter> parameters;
        public BlockStatement BlockStatement { get; private set; }
        public Function(string type, string name, List<Parameter> parametersList, BlockStatement blockStatement)
        {
            Type = type;
            Name = name;
            parameters = parametersList;
            BlockStatement = blockStatement;
        }
    }
}