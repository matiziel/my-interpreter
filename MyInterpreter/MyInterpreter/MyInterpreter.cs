using System;
using MyInterpreter.Lexer;
using MyInterpreter.Parser;
using MyInterpreter.Lexer.Tokens;
using MyInterpreter.DataSource;
using MyInterpreter.Exceptions;
using MyInterpreter.Exceptions.LexerExceptions;
using MyInterpreter.Exceptions.ParserExceptions;
using System.Text;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.StandardLibrary;

namespace MyInterpreter {
    class MyInterpreter {
        static void Main(string[] args) {
            // if (args.Length <= 0) {
            //     System.Console.WriteLine("fatal error: no input files");
            //     return;
            // }
            // var path = "../UnitTests/TestFiles/test1.ml";
            // using (var source = new FileSource(path)) {
            //     try {
            //         var scanner = new Scanner(source);
            //         var parser = new Parser.Parser(scanner);
            //         var s = parser.Parse().ToString();
            //         System.Console.WriteLine(s);

            //     }
            //     catch (LexerException e) {
            //         System.Console.WriteLine(e.Message);
            //     }
            //     catch (UnexpectedToken e) {
            //         var sb = new StringBuilder(e.Message);
            //         sb.Append(" at line: "); sb.Append(e.Position.Row);
            //         sb.Append(", column: "); sb.Append(e.Position.Column);
            //         sb.Append("\nSource:\n"); sb.Append(source.GetLineFromPosition(e.Position));
            //         sb.Append("\n");
            //         for (int i = 1; i < e.Position.Column; ++i)
            //             sb.Append(" ");
            //         sb.Append("^");
            //         System.Console.WriteLine(sb.ToString());
            //     }
            // }
            int[,] x = { { 2, 4, 2 }, { 3, 7, 5 }, { 2, 6, 4 }, { 1, 4, 6 } };
            Matrix a = new Matrix(4, 3, x);
            int[,] y = { { 2, 3 }, { 4, 5 }, { 3, 2 } };
            Matrix b = new Matrix(3, 2, y);
            System.Console.WriteLine(a * b);

        }
    }
}
