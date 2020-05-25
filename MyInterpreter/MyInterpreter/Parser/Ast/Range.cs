using MyInterpreter.Parser.Ast.Expressions;

namespace MyInterpreter.Parser.Ast {
    public class Range : Node {
        private Expression firstExpr;
        private Expression secondExpr;
        public Range(Expression first, Expression second) {
            firstExpr = first;
            secondExpr = second;
        }
        public void Accept(PrintVisitor visitor) {
            throw new System.NotImplementedException();
        }
    }
}