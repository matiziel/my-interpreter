using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Conditionals {
    public interface Conditional : Node {
        bool Evaluate(ExecEnvironment environment);
    }
}