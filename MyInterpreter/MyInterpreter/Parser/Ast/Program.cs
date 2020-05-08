using System.Collections.Generic;
using MyInterpreter.Parser.Ast.Statements;

namespace MyInterpreter.Parser.Ast
{
    public class Program
    {
        private readonly List<Function> fuctions;
        private readonly List<Definition> definitions;
        public Program()
        {
            fuctions = new List<Function>();
            definitions = new List<Definition>();
        }
        public void AddDefinition(Definition definition) => definitions.Add(definition);
        public void AddFunction(Function fuction) => fuctions.Add(fuction);
    }
}