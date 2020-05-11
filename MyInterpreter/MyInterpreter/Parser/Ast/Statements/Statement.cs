using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Statements
{
    public interface Statement
    {
        void Execute(ExecEnvironment environment);
    }
}