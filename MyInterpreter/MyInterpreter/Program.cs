using System;
using MyInterpreter.Lexer;
using MyInterpreter.Lexer.Tokens;
using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var source = new FileSource("testfile.ml"))
            {
                var scanner = new Scanner(source);
                do
                {
                    var token = scanner.Next();
                    System.Console.WriteLine(token.Type + " => " + token.ToString());
                    System.Console.WriteLine();
                } while (scanner.CurrentToken.Type != TokenType.EOT);
            }
        }
    }
}
