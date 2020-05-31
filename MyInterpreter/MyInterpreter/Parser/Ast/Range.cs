using System.Text;
using MyInterpreter.Parser.Ast.Expressions;

namespace MyInterpreter.Parser.Ast {
    public class Range {
        public Expression FirstExpr { get; private set; }
        public Expression SecondExpr { get; private set; }
        public Range(Expression first, Expression second) {
            FirstExpr = first;
            SecondExpr = second;
        }
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(FirstExpr.ToString());
            sb.Append(':');
            sb.Append(SecondExpr.ToString());
            return sb.ToString();
        }
    }
}