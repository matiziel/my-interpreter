using System.Text;
using MyInterpreter.Parser.Ast;
using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Statements;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser {
    public class PrintVisitor {
        private StringBuilder stringBuilder;
        public PrintVisitor() => stringBuilder = new StringBuilder();
        public string Value { get => stringBuilder.ToString(); }

        public void VisitFunction(Function function) {
            stringBuilder.Append("Function->");
        }
        public void VisitDefinition(Definition definition) {
            stringBuilder.Append("Definition->");
        }
        public void VisitBlockStatement(BlockStatement statement) {
            stringBuilder.Append("BlockStatement->\n");
        }
        public void VisitSimpleConditional(SimpleConditional conditional) {
            stringBuilder.Append("SimpleConditional->\n");
        }
        public void VisitConstantExpression(ConstantExpression expression) {
            stringBuilder.Append("ConstantExpression->\n");
        }
        public void VisitValueInt(Int_t value) {
            stringBuilder.Append(value.Value);
        }
    }
}