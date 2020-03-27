using static System.Console;
using static System.Math;
using System;
using System.Diagnostics;

class mainA {
	static void Main() {
		// Test that the diagonalization works as intended: 
		Write("Problem B:\n");
		Write("Problem B.i:\nTesting timing:");
		int nMax = 80;
		Stopwatch timer = new Stopwatch();		
		
		var fitDataFile = new System.IO.StreamWriter("out.dataBi.txt");

		/*
		matrix A = matrixHelp.makeRandSymMatrix(3);
		matrix V = new matrix(3, 3);
		vector e = new vector(3);
		jacobi.jacobi_cyclic(A, e, V);
		*/
		timer.Reset();
		timer.Start();
		timer.Stop();
		for(int i = 4; i < nMax; i++) {
			matrix A = matrixHelp.makeRandSymMatrix(i+1);
			matrix V = new matrix(i+1, i+1);
			vector e = new vector(i+1);
			timer.Reset();
			timer.Start();
			jacobi.jacobi_cyclic(A, e, V);
			timer.Stop();
			// Calculate the time in milliseconds:
			double time_i = ((double) timer.ElapsedTicks)/(Stopwatch.Frequency/1000);
			// Write($"i: {i}, time_i = {time_i:f10} ms\n");	
			fitDataFile.Write("{0} {1:f10}\n", Log(i+1), Log(time_i));
			timer.Reset();
		}
		fitDataFile.Close();		

		// Checking dependence by a fit.
		Write($"\nCheck the dependence by doing a fit to recorded times for 4x4 to {nMax}x{nMax} random matricies:\n");
		data timingDat = new data("out.dataBi.txt", (x)=>0.0005);
		var fitFuns = new Func<double, double>[]{x=>1, x=>x};
		vector c = leastSquares.calculateC(timingDat, fitFuns);		
		Write($"Linear fit to (log(n), log(time)) suggests that the dependence is O(n^{c[1]:f4})\n");	

		// Compare times to calculate just a single value of  
		// Lets say a 20x20 matrix.
	    // Write($"\nNow comparing calculating the first eigenvalue for a {n}x{n} random matrix\n");
	 	var outfile = new System.IO.StreamWriter("out.dataBii.txt");
		for(int n = 10; n < 101; n+=10) {
			matrix Arow = matrixHelp.makeRandSymMatrix(n);
			matrix Afull = Arow.copy();
			matrix Asfull = Arow.copy();
			matrix Vrow = new matrix(n, n);
			matrix Vfull = new matrix(n, n);
			matrix Vsfull = new matrix(n,n);
			vector erow = new vector(n);
			vector efull = new vector(n);
			vector esfull = new vector(n);
			
			
			timer.Reset();
			timer.Start();
			int rowRotations = jacobi.jacobi_nRows(1, Arow, erow, Vrow);
			timer.Stop();
			double time_row = ((double) timer.ElapsedTicks)/(Stopwatch.Frequency/1000);

			timer.Reset();
			timer.Start();
			int fullRotations = jacobi.jacobi_cyclic(Afull, efull, Vfull);
			timer.Stop();
			double time_full = ((double) timer.ElapsedTicks)/(Stopwatch.Frequency/1000);

			timer.Reset();
			timer.Start();
			int sfullRotations = jacobi.jacobi_nRows(n, Asfull, esfull, Vsfull);
			timer.Stop();
			double time_sfull = ((double) timer.ElapsedTicks)/(Stopwatch.Frequency/1000);
			outfile.Write("{0} {1} {2} {3} {4} {5} {6}\n", n, rowRotations, fullRotations, sfullRotations, time_row, time_full, time_sfull);

		}
		outfile.Close();
		
	}	
}

