using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast {
    public class Parameter : Node {
        public TypeValue Type { get; private set; }
        public string Name { get; private set; }
        public Parameter(TypeValue type, string name) {
            Type = type;
            Name = name;
        }
        public void Accept(PrintVisitor visitor) {
            throw new System.NotImplementedException();
        }

    }
}