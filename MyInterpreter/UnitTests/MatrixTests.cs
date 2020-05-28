using MyInterpreter.StandardLibrary;
using Xunit;

namespace UnitTests {
    public class MatrixTests {
        [Theory]
        [InlineData()]
        public void CheckOperatorPlus(int[,] a, int[,] b, int x, int y, int[,] result) {
            Matrix ma = new Matrix(x, y, a);
            Matrix mb = new Matrix(x, y, b);
            Matrix mr = new Matrix(x, y, result);
            Assert.Equal(mr, ma + mb);
        }
        [Theory]
        [InlineData()]
        public void CheckOperatorMinus(int[,] a, int[,] b, int x, int y, int[,] result) {
            Matrix ma = new Matrix(x, y, a);
            Matrix mb = new Matrix(x, y, b);
            Matrix mr = new Matrix(x, y, result);
            Assert.Equal(mr, ma - mb);
        }
        [Theory]
        [InlineData()]
        public void CheckOperatorMultiply(int[,] a, int xa, int[,] b, int yb, int x, int[,] result) {
            Matrix ma = new Matrix(xa, x, a);
            Matrix mb = new Matrix(x, yb, b);
            Matrix mr = new Matrix(xa, yb, result);
            Assert.Equal(mr, ma * mb);
        }
        [Theory]
        [InlineData()]
        public void CheckOperatorOneArgumentativeMinus(int[,] a, int x, int y, int[,] result) {
            Matrix ma = new Matrix(x, y, a);
            Matrix mr = new Matrix(x, y, result);
            Assert.Equal(mr, -ma);
        }
        public void CheckThrow_OperatorPlus(int[,] a, int[,] b, int x, int y, int[,] result) {
            Matrix ma = new Matrix(x, y, a);
            Matrix mb = new Matrix(x, y, b);
            Matrix mr = new Matrix(x, y, result);
            Assert.Equal(mr, ma + mb);
        }
        [Theory]
        [InlineData()]
        public void CheckThrow_OperatorMinus(int[,] a, int[,] b, int x, int y, int[,] result) {
            Matrix ma = new Matrix(x, y, a);
            Matrix mb = new Matrix(x, y, b);
            Matrix mr = new Matrix(x, y, result);
            Assert.Equal(mr, ma - mb);
        }
        [Theory]
        [InlineData()]
        public void CheckThrow_Multiply(int[,] a, int xa, int[,] b, int yb, int x, int[,] result) {
            Matrix ma = new Matrix(xa, x, a);
            Matrix mb = new Matrix(x, yb, b);
            Matrix mr = new Matrix(xa, yb, result);
            Assert.Equal(mr, ma * mb);
        }

    }
}