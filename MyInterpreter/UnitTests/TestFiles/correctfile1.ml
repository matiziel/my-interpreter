int main()
{
    matrix m[4,5];
    int k;
    int i;
    for (i =0; i<4; i+=1) {
        for (k =0; k<5; k+=1){
            m[i:i, k:k] = k * i + k - i;
        }
    }
    print(m, "eloelo");

    matrix t[5,5];
    for (i = 0; i<4; i+=1) {
        for (k = 0; k<5; k+=1){
            t[i:i, k:k] = i * i + 2 * k - i;
        }
    }
    print(t,"eloelo");
    matrix x[4,5] = m * t;
    print (x,"eloelo");
    matrix f[4,5] = x + m;
    print(f);
    f = x[0:3,0:4];
    print(f);
    print(x);
    return 0;
}