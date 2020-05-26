using System;
using System.Collections.Generic;
using MyInterpreter.Lexer.Tokens;
using MyInterpreter.Parser.Ast.Operators;

namespace MyInterpreter.Parser {
    static class Mapper {
        public static Dictionary<TokenType, Func<IOperator>> GetOperators() {
            var operators = new Dictionary<TokenType, Func<IOperator>>();
            operators.Add(TokenType.ASSIGN, () => new AssignmentOperator("="));
            operators.Add(TokenType.MULTIPLY_ASSIGN, () => new AssignmentOperator("*="));
            operators.Add(TokenType.DIVIDE_ASSIGN, () => new AssignmentOperator("/="));
            operators.Add(TokenType.PLUS_ASSIGN, () => new AssignmentOperator("+="));
            operators.Add(TokenType.MINUS_ASSIGN, () => new AssignmentOperator("-="));
            operators.Add(TokenType.MODULO_ASSIGN, () => new AssignmentOperator("%="));

            operators.Add(TokenType.PLUS, () => new AdditiveOperator("+"));
            operators.Add(TokenType.MINUS, () => new AdditiveOperator("-"));

            operators.Add(TokenType.MULTIPLY, () => new MultiplicativeOperator("*"));
            operators.Add(TokenType.DIVIDE, () => new MultiplicativeOperator("/"));
            operators.Add(TokenType.MODULO, () => new MultiplicativeOperator("%"));

            operators.Add(TokenType.EQUALS, () => new EqualityOperator("=="));
            operators.Add(TokenType.NOT_EQUAL, () => new EqualityOperator("!="));
            operators.Add(TokenType.GREATER_EQUAL, () => new EqualityOperator(">="));
            operators.Add(TokenType.LESS_EQUAL, () => new EqualityOperator("<="));
            operators.Add(TokenType.GREATER, () => new EqualityOperator(">"));
            operators.Add(TokenType.LESS, () => new EqualityOperator("<"));
            return operators;
        }

    }
}