using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser.Ast.Expressions
{
    public interface Expression
    {
        //lambda 
        Value Evaluate();
    }
    //visitor dla value ?
}