using System;
using System.Collections.Generic;
using MyInterpreter.Exceptions;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Execution {
    public class ExpressionEvaluator {
        private static Dictionary<string, Func<Int_t, Int_t, Value>> intEvaluator;
        private static Dictionary<string, Func<Matrix_t, Matrix_t, Value>> matrixEvaluator;
        static ExpressionEvaluator() {
            InitIntEvaluator();
            InitMatrixEvaluator();
        }
        private static void InitIntEvaluator() {
            intEvaluator.Add("+", (Int_t a, Int_t b) => a + b);
            intEvaluator.Add("-", (Int_t a, Int_t b) => a - b);
            intEvaluator.Add("*", (Int_t a, Int_t b) => a * b);
            intEvaluator.Add("/", (Int_t a, Int_t b) => a / b);
            intEvaluator.Add("%", (Int_t a, Int_t b) => a % b);
        }
        private static void InitMatrixEvaluator() {
            matrixEvaluator.Add("+", (Matrix_t a, Matrix_t b) => a + b);
            matrixEvaluator.Add("-", (Matrix_t a, Matrix_t b) => a - b);
            matrixEvaluator.Add("*", (Matrix_t a, Matrix_t b) => a * b);
            matrixEvaluator.Add("/", (Matrix_t a, Matrix_t b) => throw new RuntimeException());
        }    
        public static Value Evaluate(Value left, Value right, IOperator o) {
            if (left.Type != right.Type)
                throw new RuntimeException();

            if(left.Type == TypeValue.Int)
                return intEvaluator[o.Operator](left as Int_t, right as Int_t);
            if (left.Type == TypeValue.Matrix)
                return intEvaluator[o.Operator](left as Int_t, right as Int_t);
            if (left.Type == TypeValue.String && o.Operator == "+")
                return (left as String_t) +  (right as String_t);
            else
                throw new RuntimeException();
        }
        public static Value GetNegative(Value value) {
            if(value.Type == TypeValue.Int)
                return -(value as Int_t);
            else if(value.Type == TypeValue.Matrix)
                return -(value as Matrix_t);
            else
                throw new RuntimeException();
        }
        public static bool EvaluateSimpleConditional(Value value1, Value value2, EqualityOperator equalityOperator) {
            throw new NotImplementedException();
        }

    }


}