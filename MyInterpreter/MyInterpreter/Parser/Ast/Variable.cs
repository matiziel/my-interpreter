using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast {
    public class Variable : Node {
        public TypeValue Type { get; private set; }
        public string Name { get; private set; }
        public Expression First { get; private set; }
        public Expression Second { get; private set; }
        public Value Value { get; set; }
        public Variable(TypeValue type, string name, Expression first = null, Expression second = null) {
            Type = type;
            Name = name;
            First = first;
            Second = second;
        }
        public void Accept(PrintVisitor visitor) {
            visitor.VisitVariable(this);
            First?.Accept(visitor);
            Second?.Accept(visitor);
        }
    }
}