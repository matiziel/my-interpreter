using System.Collections.Generic;

namespace MyInterpreter.Parser.Ast.Statements
{
    public class BlockStatement : Statement
    {
        private IEnumerable<Statement> statements;
        public BlockStatement(IEnumerable<Statement> statements) 
            => this.statements = statements;
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}