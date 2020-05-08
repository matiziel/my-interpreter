namespace MyInterpreter.Parser.Ast.Statements
{
    public class Definition : Statement
    {
        private string type;
        private string name;
        public Definition(string type, string name)
        {
            this.type = type;
            this.name = name;
        }
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}