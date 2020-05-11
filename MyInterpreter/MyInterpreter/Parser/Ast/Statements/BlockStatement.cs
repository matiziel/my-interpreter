using System.Collections.Generic;
using MyInterpreter.Execution;

namespace MyInterpreter.Parser.Ast.Statements
{
    public class BlockStatement : Statement
    {
        private IEnumerable<Statement> statements;
        public BlockStatement(IEnumerable<Statement> statements) 
            => this.statements = statements;
        public void Execute(ExecEnvironment environment)
        {
            
        }
    }
}