int factorial(int n)
{
    if(n == 0)
        return 1;
    else
        return n * factorial(n - 1); 

}
int testWhile() {
    int i = 0;
    while(i < 10) {
        if(i == 5)
            return i;
        i += 1;
    }
}
int testWhileFor() {
    int i =0;
    while(i < 10) {
        int k;
        for(k = 0; k < 10; k += 1)
        {
            if(k == 5)
                return k;
        }
        i += 1;
    }
}

int main()
{
    int n = 6;
    int x = factorial(n);
    print(x);
    int y = testWhile();
    print(y);

    int z = testWhileFor();
    print(testWhileFor());

    return 0;
}