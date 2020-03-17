using static matrix;
using static System.Console;
using static matrixHelp;

class mainA {
	static void Main() {
		probA1();
		probA2();
	}
	
	static void probA1() {
		Write("Problem A1:\n");
		var rand = new System.Random();
		int m = 2 + rand.Next(10);
		int n = m + rand.Next(10);

		matrix A = makeRandomMatrix(n, m);

		A.print("Random matrix A:");
		
		qr_gs decomposer = new qr_gs(A);
		
		matrix Q = decomposer.Q;
		matrix R = decomposer.R;
		Q.print("Decomposition Q:");
		R.print("Decomposition R:");
	
		(Q.transpose()*Q).print("Q^(T)Q:");

		(Q*R-A).print("Q*R-A");	
	}
	
	static void probA2() {
		Write("\nProblem A2:\n");
		// Need to make the tests for A2
		var rand = new System.Random();
		// Remember, square matrix:
		
		int n = 2 + rand.Next(10);
		matrix A = makeRandomMatrix(n, n);
		vector b = makeRandomVector(n);

		qr_gs decomposer = new qr_gs(A);
		
		A.print("Input matrix A. ");
		decomposer.Q.print("Decomposition Q: ");		
		decomposer.R.print("Decomposition R: ");		
		(decomposer.Q.transpose()*b).print("Q^(T)*b = ");
		vector x = decomposer.solve(b);
		
		x.print("Solution: x = ");
		
		(A*x-b).print("A*x-b = ");
	}
	
}
