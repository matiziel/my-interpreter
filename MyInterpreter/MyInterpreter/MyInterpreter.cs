using System;
using MyInterpreter.Lexer;
using MyInterpreter.Parser;
using MyInterpreter.Lexer.Tokens;
using MyInterpreter.DataSource;
using MyInterpreter.Exceptions;
using MyInterpreter.Exceptions.LexerExceptions;

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
                    var parser = new Parser.Parser(scanner);
                    parser.Parse();
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
