using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.SemanticAnalyzer;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public class ConstantExpression : PrimaryExpression
    {
        private Value value;
        public ConstantExpression(Value value)
            =>  this.value = value;
        public Value Evaluate(ExecEnvironment environment)
        {
            throw new System.NotImplementedException();
        }
    }
}