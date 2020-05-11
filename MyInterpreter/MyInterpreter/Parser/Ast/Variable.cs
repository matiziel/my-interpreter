using MyInterpreter.Parser.Ast.Expressions;

namespace MyInterpreter.Parser.Ast
{
    public class Variable
    {
        private string type;
        private string name;
        private Expression first; 
        private Expression second;
        public Variable(string type, string name, Expression first = null, Expression second = null)
        {
            this.type = type;
            this.name = name;
            this.first = first;
            this.second = second;
        }
    }
}