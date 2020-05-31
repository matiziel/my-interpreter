using System;
using System.Collections.Generic;
using MyInterpreter.Exceptions;
using MyInterpreter.Parser.Ast;
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
        public static Value Evaluate(Value left, Value right, IOperator o)
            => EvaluateByString(left, right, o.Operator);

        public static Value EvaluateArthmeticAssignment(Value varValue, Value exprValue, AssignmentOperator assignment) {
            string operation;
            if ((operation = assignment.GetOperation()) is null)
                return null;
            else
                return EvaluateByString(varValue, exprValue, operation);
        }
        private static Value EvaluateByString(Value left, Value right, string operation) {
            if (left.Type != right.Type)
                throw new RuntimeException();

            if (left.Type == TypeValue.Int)
                return intEvaluator[operation](left as Int_t, right as Int_t);
            if (left.Type == TypeValue.Matrix)
                return intEvaluator[operation](left as Int_t, right as Int_t);
            if (left.Type == TypeValue.String && operation == "+")
                return (left as String_t) + (right as String_t);
            else
                throw new RuntimeException();
        }
        public static Value EvaluateMatrixDerefVar(Value l1, Value l2, Value r1, Value r2, Variable variable) {
            if (variable.Type != TypeValue.Matrix)
                throw new RuntimeException("Cannot use [] indexers to non matrix type");

            Int_t left1 = l1 as Int_t; Int_t left2 = l2 as Int_t;
            Int_t right1 = r1 as Int_t; Int_t right2 = r2 as Int_t;

            if (left1 is null || left2 is null || right1 is null || right2 is null)
                throw new RuntimeException("Index has to be integer");
            try {
                if (left1.Value == left2.Value && right1.Value == right2.Value)
                    return new Int_t((variable.Value as Matrix_t)[left1.Value, right1.Value]);
                else
                    return new Matrix_t(
                        (variable.Value as Matrix_t).Value
                        .GetRange(left1.Value, left2.Value, right1.Value, right2.Value));
            }
            catch (Exception) {
                throw new RuntimeException("Index out of range");
            }


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