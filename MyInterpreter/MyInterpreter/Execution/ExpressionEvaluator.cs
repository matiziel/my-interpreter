using System;
using System.Collections.Generic;
using MyInterpreter.Exceptions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Execution {
    public class ExpressionEvaluator {
        private static Dictionary<string, Func<Int_t, Int_t, Value>> intEvaluator;
        private static Dictionary<string, Func<Int_t, Int_t, bool>> intComparator;
        private static Dictionary<string, Func<Matrix_t, Matrix_t, Value>> matrixEvaluator;
        private static Dictionary<string, Func<Matrix_t, Matrix_t, bool>> matrixComparator;
        private static Dictionary<string, Func<String_t, String_t, bool>> stringComparator;
        static ExpressionEvaluator() {
            InitIntDictionaries();
            InitMatrixDictionaries();
            InitStringComparator();
        }
        private static void InitIntDictionaries() {
            intEvaluator = new Dictionary<string, Func<Int_t, Int_t, Value>>();
            intComparator = new Dictionary<string, Func<Int_t, Int_t, bool>>();

            intEvaluator.Add("+", (Int_t a, Int_t b) => a + b);
            intEvaluator.Add("-", (Int_t a, Int_t b) => a - b);
            intEvaluator.Add("*", (Int_t a, Int_t b) => a * b);
            intEvaluator.Add("/", (Int_t a, Int_t b) => a / b);
            intEvaluator.Add("%", (Int_t a, Int_t b) => a % b);

            intComparator.Add("==", (Int_t a, Int_t b) => a == b);
            intComparator.Add("!=", (Int_t a, Int_t b) => a == b);
            intComparator.Add(">=", (Int_t a, Int_t b) => a >= b);
            intComparator.Add("<=", (Int_t a, Int_t b) => a <= b);
            intComparator.Add(">", (Int_t a, Int_t b) => a > b);
            intComparator.Add("<", (Int_t a, Int_t b) => a < b);
        }
        private static void InitMatrixDictionaries() {
            matrixEvaluator = new Dictionary<string, Func<Matrix_t, Matrix_t, Value>>();
            matrixComparator = new Dictionary<string, Func<Matrix_t, Matrix_t, bool>>();

            matrixEvaluator.Add("+", (Matrix_t a, Matrix_t b) => a + b);
            matrixEvaluator.Add("-", (Matrix_t a, Matrix_t b) => a - b);
            matrixEvaluator.Add("*", (Matrix_t a, Matrix_t b) => a * b);
            matrixEvaluator.Add("/", (Matrix_t a, Matrix_t b) => throw new RuntimeException());

            matrixComparator.Add("==", (Matrix_t a, Matrix_t b) => a == b);
            matrixComparator.Add("!=", (Matrix_t a, Matrix_t b) => a != b);
        }
        private static void InitStringComparator() {
            stringComparator = new Dictionary<string, Func<String_t, String_t, bool>>();

            stringComparator.Add("==", (String_t a, String_t b) => a == b);
            stringComparator.Add("!=", (String_t a, String_t b) => a != b);
        }
        public static Value Evaluate(Value left, Value right, IOperator o) {
            if (left.Type != right.Type)
                throw new RuntimeException();

            if (left.Type == TypeValue.Int)
                return intEvaluator[o.Operator](left as Int_t, right as Int_t);
            if (left.Type == TypeValue.Matrix)
                return intEvaluator[o.Operator](left as Int_t, right as Int_t);
            if (left.Type == TypeValue.String && o.Operator == "+")
                return (left as String_t) + (right as String_t);
            else
                throw new RuntimeException();
        }
        public static Value GetNegative(Value value) {
            if (value.Type == TypeValue.Int)
                return -(value as Int_t);
            else if (value.Type == TypeValue.Matrix)
                return -(value as Matrix_t);
            else
                throw new RuntimeException();
        }
        public static bool EvaluateSimpleConditional(Value left, Value right, EqualityOperator o) {
            if (left.Type != right.Type)
                throw new RuntimeException();

            if (left.Type == TypeValue.Int)
                return intComparator[o.Operator](left as Int_t, right as Int_t);
            else if (left.Type == TypeValue.Matrix && matrixComparator.ContainsKey(o.Operator))
                return matrixComparator[o.Operator](left as Matrix_t, right as Matrix_t);
            else if (left.Type == TypeValue.String && stringComparator.ContainsKey(o.Operator))
                return stringComparator[o.Operator](left as String_t, right as String_t);
            else
                throw new RuntimeException();
        }
    }
}