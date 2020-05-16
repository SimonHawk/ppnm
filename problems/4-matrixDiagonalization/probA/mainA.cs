using static System.Console;
using static System.Math;

class mainA {
	static void Main() {
		// Test that the diagonalization works as intended: 
		Write("Problem A:\n");
		Write("First, testing the Jacobi sweeping on a random matrix A:");
		int n = 5;
		matrix A = matrixHelp.makeRandSymMatrix(n);
		matrix ACopy = A.copy();
		A.print("Random matrix A: ");
		matrix V = new matrix(n, n);
		vector e = new vector(n);
		int sweeps = jacobi.jacobi_cyclic(A, e, V);
		Write($"Sweeps = {sweeps}\n");
		A.print("Reduced matrix A: ");
		e.print("Eigenvalues: ");
		V.print("Eigenvectors V: ");
		
		// Test that this diagonalizes the matrix:
		(V.transpose()*ACopy*V).print("A in the eigenbasis: ");
		
		Write("\nNow for solving the paticle in a box problem.\n");
		// Test solving hamilton:
		int N = 64;
		Write($"Construct a {N}x{N} Hamilton, and the Jacobi method to find the eigenvalues:\n");
		matrix Hbox = hamilton.boxHamilton(N);
		matrix Hcopy = Hbox.copy();
		// Hbox.print("Hamilton matrix: ");
		matrix Vbox = new matrix(N, N);
		vector ebox = new vector(N);
		sweeps = jacobi.jacobi_cyclic(Hbox, ebox, Vbox);
		// Write("Jacobi diagonalization done after {0} sweeps\n", sweeps);
		// Vbox.print($"Box potential eigenvectors: ");
		// (Vbox.transpose()*Hcopy*Vbox).print("HBox in the eigenbasis: ");
		
		// Check eigenvalues: 
		Write("Comparing some of the eigenvalues with the analytical ones:\n");
		for(int k = 0; k < N/8; k++) {
			double analytical = PI*PI*(k+1)*(k+1);
			double calculated = ebox[k];
			Write($"k: {k,4:N1}, analytical: {analytical,10:f2}, calculated: {calculated,10:f2}, difference: {calculated - analytical,8:f3}\n");
		}		
		for(int k = N/8; k < N; k += 4) {
			double analytical = PI*PI*(k+1)*(k+1);
			double calculated = ebox[k];
			Write($"k: {k,4:N1}, analytical: {analytical,10:f2}, calculated: {calculated,10:f3}, difference: {calculated - analytical,8:f3}\n");
		}		
		Write("\nThese results are mostly limited by the discreet approximation of the Hamiltonian.\n");
		
		// Write eigenvectors out so they can be plotted:
		var outfile = new System.IO.StreamWriter("out.dataA.txt");
		int nVectors = 5;
		for(int i = 0; i < 64; i++) {
			outfile.Write("{0:f16}", i*(1.0/N));
			for(int k = 0; k < 5; k++) {
				outfile.Write(" {0:f16}", Vbox[i, k]);
			}
			outfile.Write("\n");
		}
		outfile.Close();


	}	
}
