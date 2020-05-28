using System;
using MyInterpreter.StandardLibrary;
using Xunit;

namespace UnitTests {
    public class MatrixTests {
        [Fact]
        public void CheckOperatorPlus() {
            Matrix ma = new Matrix(2, 3, new int[,] { { 1, 1, 1 }, { 2, 2, 2 } });
            Matrix mb = new Matrix(2, 3, new int[,] { { 1, 1, 1 }, { 2, 2, 2 } });
            Matrix mr = new Matrix(2, 3, new int[,] { { 2, 2, 2 }, { 4, 4, 4 } });
            Assert.Equal(mr, ma + mb);
        }
        [Fact]
        public void CheckOperatorMinus() {
            Matrix ma = new Matrix(2, 3, new int[,] { { 2, 2, 2 }, { 1, 1, 1 } });
            Matrix mb = new Matrix(2, 3, new int[,] { { 1, 1, 1 }, { 2, 2, 2 } });
            Matrix mr = new Matrix(2, 3, new int[,] { { 1, 1, 1 }, { -1, -1, -1 } });
            Assert.Equal(mr, ma - mb);
        }
        [Fact]
        public void CheckOperatorMultiply() {
            Matrix ma = new Matrix(2, 3, new int[,] { { 2, 3, 2 }, { 1, 2, 1 } });
            Matrix mb = new Matrix(3, 2, new int[,] { { 2, 3 }, { 1, 5 }, { 7, 2 } });
            Matrix mr = new Matrix(2, 2, new int[,] { { 21, 25 }, { 11, 15 } });
            Assert.Equal(mr, ma * mb);
        }
        [Fact]
        public void CheckOperatorOneArgumentativeMinus() {
            Matrix ma = new Matrix(2, 3, new int[,] { { 2, 3, 2 }, { 1, 2, 1 } });
            Matrix mr = new Matrix(2, 3, new int[,] { { -2, -3, -2 }, { -1, -2, -1 } });
            Assert.Equal(mr, -ma);
        }
        [Fact]
        public void CheckThrow_OperatorPlus() {
            Matrix ma = new Matrix(2, 3);
            Matrix mb = new Matrix(2, 5);
            Assert.Throws<InvalidOperationException>(() => ma + mb);
        }
        [Fact]
        public void CheckThrow_OperatorMinus() {
            Matrix ma = new Matrix(2, 3);
            Matrix mb = new Matrix(2, 5);
            Assert.Throws<InvalidOperationException>(() => ma - mb);
        }
        [Fact]
        public void CheckThrow_Multiply() {
            Matrix ma = new Matrix(2, 3);
            Matrix mb = new Matrix(2, 5);
            Assert.Throws<InvalidOperationException>(() => ma * mb);
        }

    }
}