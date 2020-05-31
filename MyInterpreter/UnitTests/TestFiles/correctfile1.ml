int SumOfThree(int a, int b, int c)
{
    return a + b + c; #test comment 1
}

int main()
{
    int x = ((SumOfThree(1, 2, 4) + 25) * 2) / 4;
    print(x);
    matrix m[4,5];
    print(m);
    print(m[2:2,3:3]);
    print(m[0:2,2:3]);
    x += 5;
    x = 0;
    while(x < 10){
        x += 1; 
    }
    print(x);
    int i;
    for(i = 0; i < 10; i+= 1)
        x+= 1;
    print(i);
    if(i > 20)
        x = 6;
    else 
        x = 7;
    print(x);
    return 0;
}