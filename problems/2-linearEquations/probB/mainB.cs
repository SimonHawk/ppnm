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
		int n = 3 + rand.Next(6);
		matrix A = makeRandomMatrix(n, n);
		
		qr_gs decomposer = new qr_gs(A);
		
		matrix inverse = decomposer.inverse();
		Write("\nMake a random square matrix A, with a random size:\n");
		A.print("Random Matrix A: ");
		Write("\nCalculate the inverse of A:\n");
		inverse.print("Inverse of A: ");
		Write("\nCheck that A*A^(-1) is equal to the identity matrix:\n");
		(A*inverse).print("A*A^(-1): ");
	}
}
