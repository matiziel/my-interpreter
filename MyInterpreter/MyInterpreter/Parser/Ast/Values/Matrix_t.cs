using System;
using System.Text;
using MyInterpreter.StandardLibrary;

namespace MyInterpreter.Parser.Ast.Values {
    public class Matrix_t : Value {
        public Matrix Value { get; private set; }
        public TypeValue Type { get; private set; }
        public Matrix_t(int x, int y) {
            Type = TypeValue.Matrix;
            Value = new Matrix(x, y);
        }
        public override string ToString() {
            var sb = new StringBuilder();
            return sb.ToString();
        }

    }
}