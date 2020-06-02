using static System.Math;

public static class matrixHelp {

	public static vector makeRandUnitVector(int n) {
		var rand = new System.Random();
		vector v = new vector(n);
		
		for(int i = 0; i < n; i++) {
			v[i] = rand.NextDouble();
		}
		
		return v/v.norm();
	}

	public static matrix makeRandSymMatrix(int n) {
		var rand = new System.Random();
		matrix A = new matrix(n, n);
		
		for(int i = 0; i < n; i++) {
			A[i, i] = rand.NextDouble();
		}
		for(int i = 0; i < n; i++) {
			for(int j = i+1; j < n; j++) {
				double Aij = rand.NextDouble();
				A[i,j] = Aij;
				A[j,i] = Aij;
			}
		}
		return A;
	} 

	public static double vectorDotColumn(vector v, matrix A, int col) {
		double sum = 0;
		for(int i = 0; i < v.size; i ++) {
			sum += v[i]*A[i, col];
		}
		return sum;
	} 

	// Adaptation of the smart vector norm from the vector class to work
	// for the vector being a column of a matrix:	
	public static double normColumn(matrix A, int col) {
		double meanabs=0;
		for(int i=0;i<A.size1;i++) meanabs += Abs(A[i,col]);
		meanabs /= A.size1;
		double sum=0;
		for(int i=0;i<A.size1;i++)sum+=(A[i,col]/meanabs)*(A[i,col]/meanabs);
		return meanabs*Sqrt(sum);
	}
	

}
