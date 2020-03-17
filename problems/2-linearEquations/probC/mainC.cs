using static matrix;
using static System.Console;
using static matrixHelp;

class mainC {
	static void Main() {
		probC();
	}
	
	static void probC() {
		Write("Problem C:\n");
		int n = 5; 

		matrix A = makeRandomMatrix(n, n);

		A.print("Random matrix A:");
		
		qr_givens decomposer = new qr_givens(A);
		
		vector b = makeRandomVector(n);
		
		vector x = decomposer.solve(b);
		
		b.print("Random vector b: ");
		x.print("Solution to A*x=b: ");
		(A*x-b).print("A*x-b = ");
	}
	
}
