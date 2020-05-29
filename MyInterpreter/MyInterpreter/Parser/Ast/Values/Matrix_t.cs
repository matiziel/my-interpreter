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
        public Matrix_t(Matrix value) {
            Type = TypeValue.Matrix;
            Value = value;
        }

        public override string ToString() =>
            Value.ToString();

        public override bool Equals(object obj) =>
            Value.Equals(obj);
        
        public override int GetHashCode() =>
            Value.GetHashCode();

        public static Matrix_t operator +(Matrix_t a, Matrix_t b) => 
            new Matrix_t(a.Value + b.Value);
        public static Matrix_t operator -(Matrix_t a, Matrix_t b) => 
            new Matrix_t(a.Value - b.Value);
        public static Matrix_t operator *(Matrix_t a, Matrix_t b) => 
            new Matrix_t(a.Value * b.Value);
        public static Matrix_t operator -(Matrix_t a) => 
            new Matrix_t(-a.Value);
        public static bool operator ==(Matrix_t a, Matrix_t b) => 
            a.Value == b.Value;
        public static bool operator !=(Matrix_t a, Matrix_t b) => 
            a.Value != b.Value;
    }
}