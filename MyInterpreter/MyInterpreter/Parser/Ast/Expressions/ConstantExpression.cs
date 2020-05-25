using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Expressions {
    public class ConstantExpression : PrimaryExpression {
        private Value value;
        public ConstantExpression(Value value) => this.value = value;
        public Value Evaluate(ExecEnvironment environment) => value;
        public void Accept(PrintVisitor visitor) {
            value.Accept(visitor);
        }
    }
}