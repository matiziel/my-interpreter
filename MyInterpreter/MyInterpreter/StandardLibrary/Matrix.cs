using System;
using System.Text;

namespace MyInterpreter.StandardLibrary {
    public class Matrix {
        private int[,] matrix;
        int xSize;
        int ySize;
        public Matrix(int x, int y) {
            xSize = x;
            ySize = y;
            matrix = new int[x, y];
        }
        public Matrix(int x, int y, int[,] matrix) {
            xSize = x;
            ySize = y;
            this.matrix = matrix;
        }
        public int this[int x, int y] {
            get => matrix[x, y];
            set => matrix[x, y] = value;
        }
        public Matrix GetRange(int x1, int x2, int y1, int y2) {
            if (x2 - x1 < 0 || y2 - y1 < 0)
                throw new IndexOutOfRangeException("Range cannot be negative");
            if (x2 >= xSize || y2 >= ySize)
                throw new IndexOutOfRangeException("Range cannot be negative");

            int[,] newMatrix = new int[x2 - x1 + 1, y2 - y1 + 1];
            for (int i = x1; i <= x2; ++i)
                for (int k = y1; k <= y2; ++k)
                    newMatrix[i - x1, k - y1] = matrix[i, k];
            return new Matrix(x2 - x1 + 1, y2 - y1 + 1, newMatrix);
        }
        public override string ToString() {
            var sb = new StringBuilder();
            for (int i = 0; i < xSize; ++i) {
                sb.Append('[');
                for (int k = 0; k < ySize; ++k) {
                    sb.Append(matrix[i, k]);
                    if (k != ySize - 1)
                        sb.Append(',');
                }
                sb.Append("]\n");
            }
            return sb.ToString();
        }
    }
}