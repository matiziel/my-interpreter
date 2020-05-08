using System.Collections.Generic;
using MyInterpreter.Parser.Ast.Statements;

namespace MyInterpreter.Parser.Ast
{
    public class Function
    {
        private string type;
        private string name;
        private List<Parameter> parameters;
        private BlockStatement blockStatement;
        public Function(string type, string name, List<Parameter> parameters, BlockStatement blockStatement)
        {
            this.type = type;
            this.name = name;
            this.parameters = parameters;
            this.blockStatement = blockStatement;
        }
    }
}