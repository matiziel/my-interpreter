using System;
using MyInterpreter.Lexer;
using MyInterpreter.Lexer.Tokens;
using MyInterpreter.Lexer.DataSource;
using MyInterpreter.Exceptions;

namespace MyInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if(args.Length <= 0)
                {
                    System.Console.WriteLine("fatal error: no input files");
                    return;
                }
                using (var source = new FileSource(args[0]))
                {
                    var scanner = new Scanner(source);
                    do
                    {
                        scanner.Next();
                        System.Console.WriteLine(scanner.CurrentToken.Type + " => " + scanner.CurrentToken.ToString());
                    } while (scanner.CurrentToken.Type != TokenType.EOT);
                }
            }
            catch(UnrecognizedToken e)
            {
                System.Console.WriteLine(e.Message);
            }
            catch(TooLongIdentifier e)
            {
                System.Console.WriteLine(e.Message);
            }
            
        }
    }
}
