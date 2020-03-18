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
		int m = 2 + rand.Next(6);
		int n = m + rand.Next(6);
		Write("First I create a random tall matrix with random dimmensions\n");
		matrix A = makeRandomMatrix(n, m);

		A.print("Random matrix A:");
		
		qr_gs decomposer = new qr_gs(A);
		
		Write("\nThen I decompose it into Q and R using the Gramm Schmitt method trough my qr_gs class:\n");		
		matrix Q = decomposer.Q;
		matrix R = decomposer.R;
		Q.print("Decomposition Q = ");
		R.print("Decomposition R = ");
	
		Write("\nCheck that Q^(T)Q is equal to the identity matrix:\n");
		(Q.transpose()*Q).print("Q^(T)Q = ");
		Write("\nCheck that Q*R = A, or equivalently that Q*R-A is a matrix of all zeros:\n");
		(Q*R-A).print("Q*R-A");	
	}
	
	static void probA2() {
		Write("\nProblem A2:\n");
		// Need to make the tests for A2
		var rand = new System.Random();
		// Remember, square matrix:
		Write("\nMake a random square matrix, A, with random dimmensions, and a random vector, b, of the same dimmension:\n");
		int n = 2 + rand.Next(10);
		matrix A = makeRandomMatrix(n, n);
		vector b = makeRandomVector(n);
		A.print("A = ");
		b.print("b = ");

		qr_gs decomposer = new qr_gs(A);
		
		Write("\nSolve the system A*x = b for the vector x:\n");
		//decomposer.Q.print("Decomposition Q: ");		
		//decomposer.R.print("Decomposition R: ");		
		//(decomposer.Q.transpose()*b).print("Q^(T)*b = ");
		vector x = decomposer.solve(b);
		
		x.print("Solution: x = ");
		Write("\nCheck that the vector x satisfies A*x=b:\n");
		(A*x-b).print("A*x-b = ");
	}
	
}
