using MyInterpreter.Exceptions.ParserExceptions;
using MyInterpreter.Lexer;
using MyInterpreter.Parser;
using Xunit;

namespace UnitTests {
    public class ParserTests {

        [Theory]
        [InlineData(
        @"int main()
{
    int x = 4 * (-(2+3)-(5*6)*5)/(13 % 9); 
}",
        @"Program->
Function->Int->main
Parameters->
BlockStatement->
Definition->Int->x
Value->4*(-(2+3)-(5*6)*5)/(13%9)
")]
        [InlineData(
        @"void main() {
    matrix m[4,5];
    m[0:2, 1:4] += m[0:2, 1:4];
}",
        @"Program->
Function->Void->main
Parameters->
BlockStatement->
Definition->Matrix->m[4,5]
Value->
Assignment->m[0:2,1:4]+=m[0:2,1:4]
")]
        [InlineData(
        @"int main() {
    int x = 4 * (-(2+3)+(5*6)*5)/(13 % 9);
    if(!(x > 5) || (x < 6) && !(x > 7) )
        x = 4;
    else 
        x = 5;
}",
        @"Program->
Function->Int->main
Parameters->
BlockStatement->
Definition->Int->x
Value->4*(-(2+3)+(5*6)*5)/(13%9)
if->!(x>5) || (x<6) && !(x>7)
Assignment->x=4
else->
Assignment->x=5
")]
        [InlineData(
        @"int main() {
    int x = 4 * (-(2+3)+(5*6)*5)/(13 % 9);
    while(x < 6) {
        print(x);
        x+=1;
    }
}",
        @"Program->
Function->Int->main
Parameters->
BlockStatement->
Definition->Int->x
Value->4*(-(2+3)+(5*6)*5)/(13%9)
while->x<6
BlockStatement->
FunctionCall->print
Arguments->x
Assignment->x+=1
")]
        [InlineData(
        @"int main() {
    for(i = 0; i < 5; i += 1)
    {
       print(i);         
    }
}",
        @"Program->
Function->Int->main
Parameters->
BlockStatement->
for->
first->
Assignment->i=0
conditional->i<5
second->
Assignment->i+=1
BlockStatement->
FunctionCall->print
Arguments->i
")]
        [InlineData(
        @"int main() {
    return 0;
}",
        @"Program->
Function->Int->main
Parameters->
BlockStatement->
return->0
")]
        [InlineData(
        @"int SumOfThree(int a, int b, int c) {
    return a + b + c; #test comment 1
}
int main() {
    int x = ((SumOfThree(1, 2, 4) + 25) * 2) / 4;
}",
        @"Program->
Function->Int->SumOfThree
Parameters->Int->a,Int->b,Int->c
BlockStatement->
return->a+b+c
Function->Int->main
Parameters->
BlockStatement->
Definition->Int->x
Value->((FunctionCall->SumOfThree
Arguments->1,2,4
+25)*2)/4
")]
        [InlineData(
        "int main() { string x = \"abvd\";}",
        @"Program->
Function->Int->main
Parameters->
BlockStatement->
Definition->String->x
Value->abvd
")]
        public void CheckAstTree_FromString(string text, string expected) {
            var scanner = new Scanner(new StringSource(text));
            var parser = new Parser(scanner);
            string actual = parser.Parse().ToString();
            Assert.Equal(expected, actual);
        }
    }
}