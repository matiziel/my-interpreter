matrix fillMatrix(matrix m, int x, int y) {
    int i;
    int k;
    for (i =0; i<x; i+=1) {
        for (k =0; k<y; k+=1){
            m[i:i, k:k] = k * i + k - i;
        }
    }
    print(m);
    return m;
}

int main()
{
    matrix m[4,5];
    matrix f[3,3];
    
    int k;
    int i;

    
    matrix y[4,5];
    fillMatrix(y[0:3, 0:4], 4, 5);
    print("y =", y);

    f = m[1:3, 0:2];
    matrix z[4,5] = fillMatrix(m, 4, 5);
    print("z =",z); 
    print("f= ",f);

    f = m[1:3, 0:2];
    
    print("f  =", f);
    print("z =", z);
    print("m = ", m);

    return 0;
}