using System;
using MyInterpreter.Exceptions;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Execution
{
    public class ExpressionVisitor
    {
        public static Value VisitAdditiveExpression() {
            throw new NotImplementedException();
        }
         
    }
}