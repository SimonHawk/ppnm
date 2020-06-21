using static System.Math;
using static System.Console;
using System;
using System.Diagnostics;

public class convergence {

	public static void Main() {
		// Setup the output file:
		var outfile = new System.IO.StreamWriter("data.runtime.txt");
		var timer = new Stopwatch();

		int m = 10;

		for (int n = 10; n < 150; n += 5) {
		
			matrix A = matrixHelp.makeRandSymMatrix(n);
			matrix A_m = A.copy();
		
			matrix E_jac = new matrix(n, n);
			vector e_jac = new vector(n);
			timer.Reset();
			timer.Start();
			jacobi.values(A, e_jac, 1, E_jac);
			timer.Stop();
			double time_jac = timer.ElapsedMilliseconds;			

			outfile.Write("{0} {1}\n", n, time_jac);
			
		}

		outfile.Write("\n\n");

		for (int n = 200; n < 1500; n+=10) {
		
			matrix A_m = matrixHelp.makeRandSymMatrix(n);

			matrix E_m = new matrix(n, m);
			vector e_m = new vector(m);

			timer.Reset();
			timer.Start();	
			lanczos.eigenvalues(A_m, E_m, e_m);
			timer.Stop();		

			// Save time in ms:
			double time = timer.ElapsedMilliseconds;			

			outfile.Write("{0} {1}\n", n, time);
			
		}

		outfile.Close();

	}

}


