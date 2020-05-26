using System.Text;
using MyInterpreter.Parser.Ast.Expressions;

namespace MyInterpreter.Parser.Ast {
    public class Range {
        private Expression firstExpr;
        private Expression secondExpr;
        public Range(Expression first, Expression second) {
            firstExpr = first;
            secondExpr = second;
        }
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append('[');
            sb.Append(firstExpr.ToString());
            sb.Append(':');
            sb.Append(secondExpr.ToString());
            sb.Append(']');
            return sb.ToString();
        }
    }
}