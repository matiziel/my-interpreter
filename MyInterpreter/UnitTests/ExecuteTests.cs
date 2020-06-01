using MyInterpreter.DataSource;
using MyInterpreter.Exceptions.ExecutionException;
using MyInterpreter.Lexer;
using MyInterpreter.Parser;
using Xunit;

namespace UnitTests {
    public class ExecuteTests {

        [Theory]
        [InlineData("../../../TestFiles/correctfile1.ml")]
        [InlineData("../../../TestFiles/correctfile2.ml")]
        [InlineData("../../../TestFiles/correctfile3.ml")]
        public void CheckExecuteResults(string path) {
            var parser = new Parser(new Scanner(new FileSource(path)));
            var program = parser.Parse();
            var result = program.Execute();
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData("../../../TestFiles/wrongfile3.ml")]
        public void CheckExecuteThrowsRuntime(string path) {
            var parser = new Parser(new Scanner(new FileSource(path)));
            var program = parser.Parse();
            Assert.Throws<RuntimeException>(() => program.Execute());
        }

        [Theory]
        [InlineData("../../../TestFiles/wrongfile2.ml")]
        public void CheckExecuteThrowsArthmetic(string path) {
            var parser = new Parser(new Scanner(new FileSource(path)));
            var program = parser.Parse();
            Assert.Throws<ArthmeticOperationException>(() => program.Execute());
        }
        [Theory]
        [InlineData("../../../TestFiles/wrongfile1.ml")]
        public void CheckExecuteThrowsEnvironment(string path) {
            var parser = new Parser(new Scanner(new FileSource(path)));
            var program = parser.Parse();
            Assert.Throws<EnvironmentException>(() => program.Execute());
        }

    }
}