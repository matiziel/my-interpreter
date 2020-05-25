using MyInterpreter.DataSource;

namespace MyInterpreter.Lexer.Tokens {
    public class Number : Token {
        public int Value { get; private set; }
        public Number(int value, TextPosition position) : base(TokenType.NUMBER) {
            Value = value;
            int offset = Value.ToString().Length;
            Position = new TextPosition(position, Value.ToString().Length);
        }
        public override string ToString() => Value.ToString();
    }
}