using System;
using System.Text;
using MyInterpreter.StandardLibrary;

namespace MyInterpreter.Parser.Ast.Values {
    public class Matrix_t : Value {
        private Matrix matrix;
        public TypeValue Type { get; private set; }
        public Matrix_t() {
            Type = TypeValue.Matrix;
        }
        public override string ToString() {
            var sb = new StringBuilder();
            return sb.ToString();
        }

    }
}