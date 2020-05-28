using System;
using MyInterpreter.Exceptions;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Execution {
    public class ExpressionVisitor {
        public static Value VisitAdditiveExpression(AdditiveExpression expression, ExecEnvironment environment) {
            var left = expression.leftExpression.Evaluate(environment);
            var right = expression.rightExpression.Evaluate(environment);

            if (left.Type != right.Type)
                throw new RuntimeException();
            
            if (expression.additiveOperator.Operator == "+") {
                if(left.Type == TypeValue.Int)
                    return new Int_t((left as Int_t).Value + (right as Int_t).Value);
                else if(left.Type == TypeValue.Matrix)
                    return new Matrix_t((left as Matrix_t).Value + (right as Matrix_t).Value);
                else if(left.Type == TypeValue.String)
                    return new String_t((left as String_t).Value + (right as String_t).Value);
                else
                    throw new RuntimeException();
            }
            else if(expression.additiveOperator.Operator == "-") {
                if (left.Type == TypeValue.Int)
                    return new Int_t((left as Int_t).Value - (right as Int_t).Value);
                else if (left.Type == TypeValue.Matrix)
                    return new Matrix_t((left as Matrix_t).Value - (right as Matrix_t).Value);
                else
                    throw new RuntimeException();
            }
            else
                throw new RuntimeException();

        }

    }
}