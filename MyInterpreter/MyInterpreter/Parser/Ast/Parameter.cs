namespace MyInterpreter.Parser.Ast
{
    public class Parameter
    {
        public string Type { get; private set; }
        public string Name { get; private set; }
        public Parameter(string type, string name)
        {
            Type = type;
            Name = name;
        }
        
    }
}