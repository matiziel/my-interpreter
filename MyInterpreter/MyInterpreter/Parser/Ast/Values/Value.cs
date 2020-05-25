namespace MyInterpreter.Parser.Ast.Values {
    public interface Value {
        TypeValue Type { get; }
    }
    public enum TypeValue {
        Int, String, Void, Matrix
    }
}