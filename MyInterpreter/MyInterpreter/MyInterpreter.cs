using System;
using MyInterpreter.Lexer;
using MyInterpreter.Structures.Tokens;
using MyInterpreter.DataSource;
using MyInterpreter.Exceptions;

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
            try
            {
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
