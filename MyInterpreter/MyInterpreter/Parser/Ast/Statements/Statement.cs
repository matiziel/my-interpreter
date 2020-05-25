using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Statements {
    public interface Statement : Node {
        void Execute(ExecEnvironment environment);
    }
}