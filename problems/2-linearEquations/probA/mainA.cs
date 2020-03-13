using static matrix;
using static System.Console;

class mainA {
	static void Main() {
		probA1();
	}
	
	static void probA1() {
		var rand = new System.Random();
		int m = 2 + rand.Next(10);
		int n = m + rand.Next(10);
		matrix A = makeRandomMatrix(n, m);
		A.print("Random matrix A:");
		
		
		qr_gs decomposer = new qr_gs(A);
		
		Write("Column Inner product: {0}\n", decomposer.coulumnInnerProduct(A, 0, A, 2));
		matrix Q = decomposer.Q;
		matrix R = decomposer.R;
		Q.print("Decomposition Q:");
		R.print("Decomposition R:");
	
		(Q.transpose()*Q).print("Q^(T)Q:");

		(Q*R-A).print("Q*R-A");	
	}
	
	static void probA2() {
		// Need to make the tests for A2
	}
	
	static matrix makeRandomMatrix(int n, int m) {
		var rand = new System.Random();

		matrix A = new matrix(n, m);
		
		for(int i = 0; i < n; i++) {
			for(int j =0; j < m; j++) {
				A[i, j] = 10*rand.NextDouble();
			}
		}
		return A;
	}
	
	static vector makeRandomVector(int n) {
		var rand =  new System.Random();
		vector v = new vector(n);
		for(int i = 0; i < n; i++) {
			v[i] = 5*rand.NextDouble();
		}
		return v;
	}

	static matrix makeTestMatrix() {
		var rand = new System.Random();
		int m = 3;
		int n = 3;
		
		matrix A = new matrix(n, m);
		
		for(int i = 0; i < n; i++) {
			for(int j =0; j < m; j++) {
				A[i, j] = i+j;
			}
		}
		return A;
	}
}
