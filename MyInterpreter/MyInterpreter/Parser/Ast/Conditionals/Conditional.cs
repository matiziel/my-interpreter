using MyInterpreter.SemanticAnalyzer;

namespace MyInterpreter.Parser.Ast.Conditionals
{
    public interface Conditional
    {
        bool Evaluate(ExecEnvironment environment);
    }
}