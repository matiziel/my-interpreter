using MyInterpreter.Lexer;
using MyInterpreter.Parser.Ast;

namespace MyInterpreter.Parser
{
    public class Parser
    {
        private readonly IScanner _scanner;

        public Parser(IScanner scanner)
        {
            _scanner = scanner;
        }

        public Program Parse()
        {
            return new Program();
        }
    }
}