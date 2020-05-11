using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public interface Expression
    {
        Value Evaluate(ExecEnvironment environment);
    }
}