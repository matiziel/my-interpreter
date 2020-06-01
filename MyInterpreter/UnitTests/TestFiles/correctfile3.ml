

int main()
{
    matrix m[4,5];
    m[0:0, 1:1] = 6;

    matrix t[4,5];
    t[0:0, 2:2] = 8;
    matrix x[4,5] = m - t;
    print(x);
    return 0;
}