using System;
using System.Text;
using MyInterpreter.Parser.Ast;
using MyInterpreter.Parser.Ast.Conditionals;
using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Parser.Ast.Statements;
using MyInterpreter.Parser.Ast.Values;

namespace MyInterpreter.Parser {
    public class PrintVisitor {
        private StringBuilder stringBuilder;
        public PrintVisitor() => stringBuilder = new StringBuilder();
        public string Value { get => stringBuilder.ToString(); }
        public void VisitProgram(Program program) =>
            stringBuilder.Append("Program->\n");
        
        public void VisitFunction(Function function) {
            stringBuilder.Append("Function->");
            stringBuilder.Append(function.Type);
            stringBuilder.Append("->");
            stringBuilder.Append(function.Name);
            stringBuilder.Append("\n");
        }
        public void VisitAndConditional(AndConditional andConditional) =>
            stringBuilder.Append(" && ");
        
        public void VisitOrConditional(OrConditional orConditional) =>
            stringBuilder.Append(" || ");
        
        public void VisitParenConditional(ParenConditional conditional, char paren) =>
            stringBuilder.Append(paren);

        public void VisitParenExpression(ParenExpression expression, char paren) =>
            stringBuilder.Append(paren);
        public void VisitNegatedExpression(UnaryExpression expression) =>
            stringBuilder.Append("-");
        public void VisitOperator(IOperator ioperator) =>
            stringBuilder.Append(ioperator.Operator);
    
        public void VisitDefinition(Definition definition) =>
            stringBuilder.Append("Definition->");

        public void VisitParameter(Parameter parameter) {
            stringBuilder.Append("Parameter->");
            stringBuilder.Append(parameter.Type);
            stringBuilder.Append("->");
            stringBuilder.Append(parameter.Name);
            stringBuilder.Append("\n");
        }
        public void VisitVariable(Variable variable) {
            stringBuilder.Append("Variable->");
            stringBuilder.Append(variable.Type);
            stringBuilder.Append("->");
            stringBuilder.Append(variable.Name);
            stringBuilder.Append("\n");
        }
        public void VisitBlockStatement(BlockStatement statement) =>
            stringBuilder.Append("BlockStatement->\n");

        
        
        public void VisitValueInt(Int_t value) =>
            stringBuilder.Append(value.Value);
        
        public void VisitValueString(String_t value) {
            stringBuilder.Append('\"');
            stringBuilder.Append(value.Value);
            stringBuilder.Append('\"');
        }
        public void VisitValueVoid(Void_t value) =>
            stringBuilder.Append(value.Type);
        
        
    }
}