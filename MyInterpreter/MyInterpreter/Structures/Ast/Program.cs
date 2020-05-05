using System.Collections.Generic;
namespace MyInterpreter.Structures.Ast
{
    public class Program
    {
        private readonly List<FunctionDefinition> fuctionDefinitions;
        private readonly List<Definition> definitions;
        public Program()
        {
            fuctionDefinitions = new List<FunctionDefinition>();
            definitions = new List<Definition>();
        }
        public void AddDefinition(Definition definition) => definitions.Add(definition);
        public void AddFunctionDefinition(FunctionDefinition fuctionDefinition) => fuctionDefinitions.Add(fuctionDefinition);
    }
}