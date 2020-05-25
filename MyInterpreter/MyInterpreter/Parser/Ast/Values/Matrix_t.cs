namespace MyInterpreter.Parser.Ast.Values {
    public class Matrix_t : Value {
        public TypeValue Type { get; private set; }
        public Matrix_t() {
            Type = TypeValue.Matrix;
        }
    }
}