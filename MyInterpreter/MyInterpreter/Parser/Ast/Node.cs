namespace MyInterpreter.Parser.Ast {
    public interface Node {
        void Accept(PrintVisitor visitor);
    }
}