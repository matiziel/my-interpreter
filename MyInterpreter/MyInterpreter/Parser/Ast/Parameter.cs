using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast
{
    public class Parameter
    {
        public TypeValue Type { get; private set; }
        public string Name { get; private set; }
        public Parameter(TypeValue type, string name)
        {
            Type = type;
            Name = name;
        }
        
    }
}