using System;
using MyInterpreter.Lexer;
using MyInterpreter.Parser;
using MyInterpreter.Lexer.Tokens;
using MyInterpreter.DataSource;
using MyInterpreter.Exceptions;
using MyInterpreter.Exceptions.LexerExceptions;
using MyInterpreter.Exceptions.ParserExceptions;
using System.Text;

namespace MyInterpreter
{
    class MyInterpreter
    {
        static void Main(string[] args)
        {
            if(args.Length <= 0)
            {
                System.Console.WriteLine("fatal error: no input files");
                return;
            }
            using (var source = new FileSource(args[0]))
            {
                try
                {
                    var scanner = new Scanner(source);
                    var parser = new Parser.Parser(scanner);
                    parser.Parse();
                }
                catch(LexerException e)
                {
                    System.Console.WriteLine(e.Message);
                }
                catch(UnexpectedToken e)
                {
                    var sb = new StringBuilder(e.Message);
                    sb.Append(" at line: "); sb.Append(e.Position.Row);
                    sb.Append(", column: "); sb.Append(e.Position.Column);
                    sb.Append("\nSource:\n"); sb.Append(source.GetLineFromPosition(e.Position));
                    sb.Append("\n");
                    for(int i = 1; i < e.Position.Column; ++i)
                        sb.Append(" ");
                    sb.Append("^");
                    System.Console.WriteLine(sb.ToString());
                }
            }
        }
    }
}
