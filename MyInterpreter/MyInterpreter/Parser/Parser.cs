using MyInterpreter.Lexer;

namespace MyInterpreter.Parser
{
    public class Parser
    {
        private readonly IScanner _scanner;

        public Parser(IScanner scanner)
        {
            _scanner = scanner;
        }
        
    }
}