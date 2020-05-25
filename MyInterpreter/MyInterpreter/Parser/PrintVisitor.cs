using System.Text;
using MyInterpreter.Parser.Ast.Conditionals;

namespace MyInterpreter.Parser {
    public class PrintVisitor {
        private StringBuilder stringBuilder;
        public PrintVisitor() => stringBuilder = new StringBuilder();
        public string Value { get => stringBuilder.ToString(); }

        public void VisitSimpleConditional(SimpleConditional conditional) {

        }
    }
}