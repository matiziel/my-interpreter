using MyInterpreter.Parser.Ast.Expressions;
using MyInterpreter.Parser.Ast.Operators;
using MyInterpreter.Execution;
using System.Text;
using System;
using MyInterpreter.Parser.Ast.Values;
using MyInterpreter.Exceptions.ExecutionException;

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

            if (variable.Type == TypeValue.Matrix) {
                var matrix = variable.Value as Matrix_t;
                if (derefVariable.Left != null && derefVariable.Right != null) {
                    AssignToMatrixByRange(environment, matrix, right);
                }
                else {
                    AssignToMatrix(environment, variable, right);
                }
                return;
            }
            if (variable.Type != right.Type)
                throw new RuntimeException("Cannot cast " + right.Type + " to " + variable.Type);

            variable.Value = (assignmentOperator.Operator != "=") ?
                ExpressionEvaluator.EvaluateArthmeticAssignment(variable.Value, right, assignmentOperator) :
                right;
        }

        private void AssignToMatrix(ExecEnvironment environment, Variable variable, Value right) {
            if (right.Type != TypeValue.Matrix)
                throw new RuntimeException("Cannot cast " + right.Type + " to " + TypeValue.Matrix);
            var value = right as Matrix_t;
            var matrix = variable.Value as Matrix_t;
            if (matrix.Value.SizeX != value.Value.SizeX || matrix.Value.SizeY != value.Value.SizeY)
                throw new RuntimeException("Sizes of matrices is various");
            variable.Value = value;
        }

        private void AssignToMatrixByRange(ExecEnvironment environment, Matrix_t matrix, Value value) {
            Int_t left1 = derefVariable.Left.FirstExpr.Evaluate(environment) as Int_t;
            Int_t left2 = derefVariable.Left.SecondExpr.Evaluate(environment) as Int_t;
            Int_t right1 = derefVariable.Right.FirstExpr.Evaluate(environment) as Int_t;
            Int_t right2 = derefVariable.Right.SecondExpr.Evaluate(environment) as Int_t;

            if (left1 is null || left2 is null || right1 is null || right2 is null)
                throw new RuntimeException("Index has to be integer");

            try {
                if (left1.Value == left2.Value && right1.Value == right2.Value) {
                    var intValue = value as Int_t;
                    if (intValue is null)
                        throw new RuntimeException("Cannot assign to int variable");
                    matrix[left1.Value, right1.Value] = intValue.Value;
                }
                else
                    throw new ArthmeticOperationException("Matrix with range can be only r-value");
            }
            catch (Exception) {
                throw new RuntimeException("Index out of range");
            }

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