using System;
using MyInterpreter.Lexer;
using MyInterpreter.Lexer.Tokens;

namespace MyInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            Token t = new Keyword(TokenType.FOR);
            Token r = new Number(1);
            Token x = new Word(TokenType.IDENTIFIER, "x2137");

            System.Console.WriteLine(t);
            System.Console.WriteLine(r);
            System.Console.WriteLine(x);
        }
    }
}
