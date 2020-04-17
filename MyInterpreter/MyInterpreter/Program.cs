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
                using (var source = new FileSource("../UnitTests/TestFiles/testfile.ml"))
                {
                    var scanner = new Scanner(source);
                    do
                    {
                        var token = scanner.Next();
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
