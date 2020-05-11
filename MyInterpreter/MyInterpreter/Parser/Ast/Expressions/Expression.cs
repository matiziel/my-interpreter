using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.SemanticAnalyzer;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public interface Expression
    {
        //lambda 
        Value Evaluate(ExecEnvironment environment);
    }
    //visitor dla value ?
}