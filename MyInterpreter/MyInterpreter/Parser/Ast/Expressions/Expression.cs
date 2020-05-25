using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Expressions {
    public interface Expression : Node {
        Value Evaluate(ExecEnvironment environment);
    }
}