namespace MyInterpreter.Parser.Ast
{
    public class Parameter
    {
        public string TypeName { get; private set; }
        public string Name { get; private set; }
        public Parameter(string typeName, string name)
        {
            TypeName = typeName;
            Name = name;
        }
        
    }
}