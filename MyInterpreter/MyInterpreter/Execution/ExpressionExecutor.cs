using System;
using System.Collections.Generic;
using MyInterpreter.Exceptions;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Execution {
    public static class ExpressionExecutor {
        public static Value EvaluateExpression(Value left, Value right, IOperator operatorValue) {
            if (left.Type != right.Type)
                throw new RuntimeException();

            switch (operatorValue.Operator) {
                case "+":
                    if (left.Type == TypeValue.Int)
                        return new Int_t((left as Int_t).Value + (right as Int_t).Value);
                    else if (left.Type == TypeValue.String)
                        return new String_t((left as String_t).Value + (right as String_t).Value);
                    else if (left.Type == TypeValue.Matrix)
                        throw new NotImplementedException();
                    else
                        throw new RuntimeException();
                case "-":
                    if (left.Type == TypeValue.Int)
                        return new Int_t((left as Int_t).Value - (right as Int_t).Value);
                    else if (left.Type == TypeValue.Matrix)
                        throw new NotImplementedException();
                    else
                        throw new RuntimeException();
                case "*":
                    if (left.Type == TypeValue.Int)
                        return new Int_t((left as Int_t).Value * (right as Int_t).Value);
                    else if (left.Type == TypeValue.Matrix)
                        throw new NotImplementedException();
                    else
                        throw new RuntimeException();
                case "/":
                    if (left.Type == TypeValue.Int)
                        return new Int_t((left as Int_t).Value / (right as Int_t).Value);
                    else
                        throw new RuntimeException();
                case "%":
                    if (left.Type == TypeValue.Int)
                        return new Int_t((left as Int_t).Value % (right as Int_t).Value);
                    else
                        throw new RuntimeException();
                default:
                    throw new RuntimeException();
            }
        }
        public static Value GetNegative(Value value) {
            if (value.Type == TypeValue.Int)
                return new Int_t((value as Int_t).Value * (-1));
            else if (value.Type == TypeValue.Matrix)
                throw new NotImplementedException();
            else
                throw new RuntimeException();
        }
        public static bool EvaluateSimpleConditional(Value left, Value right, EqualityOperator operatorValue) {
            if (left.Type != right.Type)
                throw new RuntimeException();

            if (left.Type != TypeValue.Int)
                throw new RuntimeException();
            Int_t leftValue = left as Int_t;
            Int_t rightValue = right as Int_t;
            switch (operatorValue.Operator) {
                case "==":
                    return leftValue.Value == rightValue.Value;
                case "!=":
                    return leftValue.Value != rightValue.Value;
                case ">=":
                    return leftValue.Value >= rightValue.Value;
                case "<=":
                    return leftValue.Value <= rightValue.Value;
                case ">":
                    return leftValue.Value > rightValue.Value;
                case "<":
                    return leftValue.Value < rightValue.Value;
                default:
                    throw new RuntimeException();
            }
        }
    }
}