using System;
using System.Text;

namespace MyInterpreter.StandardLibrary {
    public class Matrix {
        private int[,] matrix;
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public Matrix(int x, int y) {
            SizeX = x;
            SizeY = y;
            matrix = new int[x, y];
        }
        public Matrix(int x, int y, int[,] matrix) {
            SizeX = x;
            SizeY = y;
            this.matrix = matrix;
        }
        public int this[int x, int y] {
            get => matrix[x, y];
            set => matrix[x, y] = value;
        }
        public Matrix GetRange(int x1, int x2, int y1, int y2) {
            if (x2 - x1 < 0 || y2 - y1 < 0)
                throw new IndexOutOfRangeException("Range cannot be negative");
            if (x2 >= SizeX || y2 >= SizeY)
                throw new IndexOutOfRangeException("Range cannot be negative");

            int[,] newMatrix = new int[x2 - x1 + 1, y2 - y1 + 1];
            for (int i = x1; i <= x2; ++i)
                for (int k = y1; k <= y2; ++k)
                    newMatrix[i - x1, k - y1] = matrix[i, k];

            return new Matrix(x2 - x1 + 1, y2 - y1 + 1, newMatrix);
        }
        public static Matrix operator +(Matrix a, Matrix b) {
            if (a.SizeX != b.SizeX || a.SizeY != b.SizeY)
                throw new InvalidOperationException("Sizes of matrix is incorrect");
            Matrix newMatrix = new Matrix(a.SizeX, a.SizeY);
            for (int i = 0; i < newMatrix.SizeX; ++i)
                for (int k = 0; k < newMatrix.SizeY; ++k)
                    newMatrix[i, k] = a[i, k] + b[i, k];
            return newMatrix;
        }
        public static Matrix operator -(Matrix a, Matrix b) {
            return a + (-b);
        }
        public static Matrix operator *(Matrix a, Matrix b) {
            if (a.SizeY != b.SizeX)
                throw new InvalidOperationException("Sizes of matrix is incorrect");
            Matrix newMatrix = new Matrix(a.SizeX, b.SizeY);
            for (int i = 0; i < a.SizeX; ++i)
                for (int j = 0; j < b.SizeY; ++j)
                    for (int k = 0; k < a.SizeY; ++k) {
                        newMatrix[i, j] += a[i, k] * b[k, j];
                    }
            return newMatrix;
        }
        public static Matrix operator /(Matrix a, Matrix b) {
            throw new InvalidOperationException();
        }
        public static Matrix operator -(Matrix matrix) {
            Matrix newMatrix = new Matrix(matrix.SizeX, matrix.SizeY);
            for (int i = 0; i < newMatrix.SizeX; ++i)
                for (int k = 0; k < newMatrix.SizeY; ++k)
                    newMatrix[i, k] = -matrix[i,k];
            return newMatrix;
        }
        public static bool operator ==(Matrix a, Matrix b) {
            if (a.SizeX != b.SizeX || a.SizeY != b.SizeY)
                return false;
            for (int i = 0; i < a.SizeX; ++i) {
                for (int k = 0; k < a.SizeY; ++k) {
                    if (a[i, k] != b[i, k])
                        return false;
                }
            }
            return true;
        }
        public static bool operator !=(Matrix a, Matrix b) =>
            !(a == b);

        public override string ToString() {
            var sb = new StringBuilder();
            for (int i = 0; i < SizeX; ++i) {
                sb.Append('[');
                for (int k = 0; k < SizeY; ++k) {
                    sb.Append(matrix[i, k]);
                    if (k != SizeY - 1)
                        sb.Append(',');
                }
                sb.Append("]\n");
            }
            return sb.ToString();
        }

        public override bool Equals(object obj) {
            Matrix objMatrix = obj as Matrix;
            if (!(obj is Matrix))
                return false;
            return this == objMatrix;
        }

        public override int GetHashCode() {
            return matrix.GetHashCode();
        }
    }
}