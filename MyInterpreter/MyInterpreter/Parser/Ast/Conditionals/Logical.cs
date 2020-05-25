using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Conditionals {
    public abstract class Logical : Conditional {
        protected bool isNegated;
        public abstract bool Evaluate(ExecEnvironment environment);
        public abstract void Accept(PrintVisitor visitor);
    }
}