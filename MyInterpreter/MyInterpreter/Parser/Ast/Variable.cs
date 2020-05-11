using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast
{
    public class Variable
    {
        private string type;
        public string Name { get; private set;}
        private Expression first; 
        private Expression second;
        private Value value;
        public Variable(string type, string name, Expression first = null, Expression second = null)
        {
            this.type = type;
            Name = name;
            this.first = first;
            this.second = second;
        }
        public void SetValue(Value value) => this.value = value;
    }
}