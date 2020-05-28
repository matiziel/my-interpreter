using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;
using System.Text;
using System;

namespace MyInterpreter.Parser.Ast.Expressions {
    public class AdditiveExpression : Expression {
        public Expression leftExpression { get; private set; }
        public Expression rightExpression { get; private set; }
        public AdditiveOperator additiveOperator { get; private set; }
        public AdditiveExpression(Expression left, Expression right, AdditiveOperator operatorValue) {
            leftExpression = left;
            rightExpression = right;
            additiveOperator = operatorValue;
        }
        public AdditiveExpression(Expression left) {
            leftExpression = left;
            rightExpression = null;
            additiveOperator = null;
        }
        public Value Evaluate(ExecEnvironment environment) {
            throw new NotImplementedException();
        }
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(leftExpression.ToString());
            sb.Append(additiveOperator.ToString());
            sb.Append(rightExpression.ToString());
            return sb.ToString();
        }
    }
}