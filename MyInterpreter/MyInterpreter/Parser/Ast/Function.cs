using System.Collections.Generic;
using MyInterpreter.Parser.Ast.Statements;
using MyInterpreter.Parser.Ast.Values;
using System;

namespace MyInterpreter.Parser.Ast {
    public class Function : Node {
        private TypeValue type;
        private string name;
        private IEnumerable<Parameter> parameters;
        private BlockStatement blockStatement;
        public Function(TypeValue type, string name, IEnumerable<Parameter> parameters, BlockStatement blockStatement) {
            this.type = type;
            this.name = name;
            this.parameters = parameters;
            this.blockStatement = blockStatement;
        }
        public Value Execute() {
            throw new NotImplementedException();
        }
        public void Accept(PrintVisitor visitor) {
            throw new System.NotImplementedException();
        }
    }
}