using System.Collections.Generic;

namespace MyInterpreter.Parser.Ast.Statements
{
    public class BlockStatement : Statement
    {
        private List<Statement> statements;
        public BlockStatement() => statements = new List<Statement>();
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
        public void AddStatement(Statement statement) => statements.Add(statement);
    }
}