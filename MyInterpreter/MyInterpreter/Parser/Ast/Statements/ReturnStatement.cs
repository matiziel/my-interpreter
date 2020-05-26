using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Execution;
using System.Text;

namespace MyInterpreter.Parser.Ast.Statements {
    public class ReturnStatement : Statement {
        private Expression value;
        public ReturnStatement(Expression value) => this.value = value;

        public void Execute(ExecEnvironment environment) {
            throw new System.NotImplementedException();
        }
        public override string ToString() {
            var sb = new StringBuilder("return->");
            sb.Append(value.ToString());
            sb.Append("\n");
            return sb.ToString();
        }
        
    }
}