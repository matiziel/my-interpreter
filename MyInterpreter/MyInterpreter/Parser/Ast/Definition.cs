namespace MyInterpreter.Parser.Ast
{
    public class Definition
    {
        public string Name { get; private set; } 
        public Expression Value { get; private set; }
    }
}