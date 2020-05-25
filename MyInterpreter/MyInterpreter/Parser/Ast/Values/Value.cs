namespace MyInterpreter.Parser.Ast.Values {
    public interface Value : Node {
        TypeValue Type { get; }
    }
    public enum TypeValue {
        Int, String, Void, Matrix
    }
}