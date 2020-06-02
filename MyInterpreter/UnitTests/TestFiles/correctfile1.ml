matrix fillMatrix(matrix m, int x, int y) {
    int i;
    int k;
    for (i =0; i<x; i+=1) {
        for (k =0; k<y; k+=1){
            m[i:i, k:k] = k * i + k - i;
        }
    }
    return m;
}

int main()
{
    matrix m[4,5];
    matrix f[3,3];
    f = m[1:3, 0:2];
    int k;
    int i;
    

    matrix z[4,5] = fillMatrix(m, 4, 5);
    print(z); 
    print(f);
    matrix y[4,5] = m[0:3, 0:4];
    z[0:0,0:0] = 2137;
    f = m[1:3, 0:2];
    
    print(f);
    print(z);
    print(m);
    print(y);

    return 0;
}