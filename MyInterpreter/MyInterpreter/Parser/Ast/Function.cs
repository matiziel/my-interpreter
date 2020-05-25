using System.Collections.Generic;
using MyInterpreter.Parser.Ast.Statements;
using MyInterpreter.Parser.Ast.Values;
using System;

namespace MyInterpreter.Parser.Ast {
    public class Function : Node {
        public TypeValue Type { get; private set; }
        public string Name { get; private set; }
        private IEnumerable<Parameter> parameters;
        private BlockStatement blockStatement;
        public Function(TypeValue type, string name, IEnumerable<Parameter> parameters, BlockStatement blockStatement) {
            Type = type;
            Name = name;
            this.parameters = parameters;
            this.blockStatement = blockStatement;
        }
        public Value Execute() {
            throw new NotImplementedException();
        }
        public void Accept(PrintVisitor visitor) {
            visitor.VisitFunction(this);
            foreach (var param in parameters) {
                param.Accept(visitor);
            }
            blockStatement.Accept(visitor);
        }
    }
}