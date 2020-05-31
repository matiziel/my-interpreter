using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Execution;
using System.Text;
using System;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Exceptions;

namespace MyInterpreter.Parser.Ast.Statements {
    public class Assignment : Statement {
        private DerefVariable derefVariable;
        private AssignmentOperator assignmentOperator;
        private Expression expression;
        public Assignment(DerefVariable variable, AssignmentOperator assignmentOperator, Expression expression) {
            this.derefVariable = variable;
            this.assignmentOperator = assignmentOperator;
            this.expression = expression;
        }
        public void Execute(ExecEnvironment environment) {
            Variable variable = derefVariable.GetVariable(environment);
            Value right = expression.Evaluate(environment);
            if(variable.Type != right.Type)
                throw new RuntimeException();
            
            

            variable.Value = (assignmentOperator.Operator != "=") ?
                ExpressionEvaluator.EvaluateArthmeticAssignment(variable.Value, right, assignmentOperator) :
                right;
        }
        public override string ToString() {
            var sb = new StringBuilder("Assignment->");
            sb.Append(derefVariable.ToString());
            sb.Append(assignmentOperator.ToString());
            sb.Append(expression.ToString());
            sb.Append("\n");
            return sb.ToString();
        }
    }
}