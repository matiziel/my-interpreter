using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Conditionals {
    public interface Conditional {
        bool Evaluate(ExecEnvironment environment);
    }
}