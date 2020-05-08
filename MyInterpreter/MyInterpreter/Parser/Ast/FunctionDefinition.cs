using MyInterpreter.Parser.Ast.Statements;

namespace MyInterpreter.Parser.Ast
{
    public class FunctionDefinition
    {
        public string Type { get; private set; }
        public string Name { get; private set; }
        public BlockStatement BlockStatement { get; private set; }
        public FunctionDefinition(string type, string name, BlockStatement blockStatement)
        {
            Type = type;
            Name = name;
            BlockStatement = blockStatement;
        }
    }
}