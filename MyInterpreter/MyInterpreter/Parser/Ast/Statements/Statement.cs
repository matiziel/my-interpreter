using MyInterpreter.SemanticAnalyzer;

namespace MyInterpreter.Parser.Ast.Statements
{
    public interface Statement
    {
        void Execute(ExecEnvironment environment);
    }
}