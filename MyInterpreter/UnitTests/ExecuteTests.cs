using MyInterpreter.Exceptions;
using MyInterpreter.Lexer;
using MyInterpreter.Parser;
using Xunit;

namespace UnitTests {
    public class ExecuteTests {

        [Theory]
        [InlineData("../../../TestFiles/correctfile.ml")]
        public void CheckExecuteResults(string path) {
            var parser = new Parser(new Scanner(new StringSource(path)));
            var program = parser.Parse();
        }

        [Theory]
        [InlineData("../../../TestFiles/wrongfile.ml")]
        public void CheckExecuteThrows(string path) {
            var parser = new Parser(new Scanner(new StringSource(path)));
            var program = parser.Parse();
            Assert.Throws<RuntimeException>(() => program.Execute());
        }
    }
}