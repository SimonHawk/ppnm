using static matrix;
using static System.Console;
using static matrixHelp;

class mainA {
	static void Main() {
		probB();
	}
	
	static void probB() {
		Write("\nProblem B:\n");
		var rand =  new System.Random();
		int n = 2 + rand.Next(10);
		matrix A = makeRandomMatrix(n, n);

		qr_gs decomposer = new qr_gs(A);
		
		matrix inverse = decomposer.qr_gs_inverse();

		A.print("Random Matrix A: ");
		inverse.print("Inverse of A: ");
		(A*inverse).print("A*A^(-1): ");
	}
}
