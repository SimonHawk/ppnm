using static matrix;
using static System.Console;
using static matrixHelp;

class mainC {
	static void Main() {
		probC();
	}
	
	static void probC() {
		Write("Problem C:\n");
		var rand = new System.Random();
		int n = 5 + rand.Next(3);
		
		matrix A = makeRandomMatrix(n, n);
		
		A.print("Make a random square matrix of random size: A = ");
		
		qr_givens decomp_givens = new qr_givens(A);
		qr_gs decomp_gs = new qr_gs(A);
		
		Write("\nDo the Givens decomposition and store the restult in the matrix G, which cotains the elements of the component R in the upper triangular part, and the angles for the Givens rotations in the relevant sub-diagonal entries. Compare with the R matrix found from the Gramm-Schmitt method:");
		decomp_givens.G.print("Givens G: ");
		decomp_gs.R.print("Gram-Schmitt R: ");
		Write("The upper triangular parts are the same.\n");
		
		vector b = makeRandomVector(n);
		b.print("\nMake a random vector b with the same size as A: b = ");
	
		Write("\nCompare using the Givens rotations to apply Q^(T) to b, and doing the explicit matrix multiplication with the Q matrix found by the Gramm-Schmitt method:\n");
		decomp_givens.applyQT(b).print("Givens: Q^(T)*b = ");
		(decomp_gs.Q.transpose()*b).print("GS: Q^(T)*b = ");
		
		vector x = decomp_givens.solve(b);
		
		x.print("\nSolution to A*x=b using Givens: ");
		(A*x-b).print("\nCheck solution satisfies A*x = b: A*x-b = ");
		
		Write("\nFind the inverse of A, and check that A^(-1)*A is the identity matrix:\n");
		decomp_givens.inverse().print("A^(-1)");
		(decomp_givens.inverse()*A).print("A^(-1)*A = ");


	}
	
}
