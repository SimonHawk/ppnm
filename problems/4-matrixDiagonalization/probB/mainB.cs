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

		//probB3();
		//probB4();
		probB5();
	}	
	
	// Comparison of time and rotations needed to calculate the lowest
	// eigenvalue for the cyclic and single value method.
	static void probB3() {
		var timer = new Stopwatch();
		timer.Start();
		var outfile = new System.IO.StreamWriter("out.probB3.txt");
		int NMax = 200;
		int step = 10;
		for(int n = NMax/2; n <= NMax; n+=step) {
			// 1: Cyclic sweep
			// 2: Single row
			matrix A1 = matrixHelp.makeRandSymMatrix(n);
			matrix A2 = A1.copy();
			
			matrix V1 = new matrix(n,n);
			matrix V2 = new matrix(n,n);
		
			vector e1 = new vector(n);
			vector e2 = new vector(n);

			timer.Restart();
			int rot1 = jacobi.jacobi_cyclic(A1, e1, V1);
			timer.Stop();
			double time1 = timer.ElapsedMilliseconds;

			timer.Restart();
			int rot2 = jacobi.jacobi_nRows(1, A2, e2, V2, true);
			timer.Stop();
			double time2 = timer.ElapsedMilliseconds;

			outfile.Write("{0} {1} {2} {3} {4} {5} {6}\n", n, time1, time2, rot1, rot2, e1[0], e2[0]);
		}		
		outfile.Close();

	}	

	// Comparison of time and rotations needed to calculate all
	// eigenvalues for the cyclic and single value method.
	static void probB4() {
		var timer = new Stopwatch();
		timer.Start();
		var outfile = new System.IO.StreamWriter("out.probB4.txt");
		int NMax = 100;
		int step = 5;
		for(int n = NMax/2; n <= NMax; n+=step) {
			// 1: Cyclic sweep
			// 2: Single row
			matrix A1 = matrixHelp.makeRandSymMatrix(n);
			matrix A2 = A1.copy();
			
			matrix V1 = new matrix(n,n);
			matrix V2 = new matrix(n,n);
		
			vector e1 = new vector(n);
			vector e2 = new vector(n);

			timer.Restart();
			int rot1 = jacobi.jacobi_cyclic(A1, e1, V1);
			timer.Stop();
			double time1 = timer.ElapsedMilliseconds;

			timer.Restart();
			int rot2 = jacobi.jacobi_nRows(n, A2, e2, V2, true);
			timer.Stop();
			double time2 = timer.ElapsedMilliseconds;

			outfile.Write("{0} {1} {2} {3} {4} {5} {6}\n", n, time1, time2, rot1, rot2, e1[0], e2[0]);
		}		
		outfile.Close();

	}	

	// Comparison of time and rotations needed to calculate the lowest
	// eigenvalue for the cyclic and single value method.
	static void probB5() {
		var timer = new Stopwatch();
		timer.Start();
		var outfile = new System.IO.StreamWriter("out.probB5.txt");
		int NMax = 200;
		int step = 10;
		for(int n = NMax/2; n <= NMax; n+=step) {
			// 1: Cyclic sweep
			// 2: Single row
			matrix A1 = matrixHelp.makeRandSymMatrix(n);
			matrix A2 = A1.copy();
			
			matrix V1 = new matrix(n,n);
			matrix V2 = new matrix(n,n);
		
			vector e1 = new vector(n);
			vector e2 = new vector(n);

			timer.Restart();
			int rot1 = jacobi.jacobi_cyclic(A1, e1, V1);
			timer.Stop();
			double time1 = timer.ElapsedMilliseconds;

			timer.Restart();
			int rot2 = jacobi.jacobi_nRows(1, A2, e2, V2, false);
			timer.Stop();
			double time2 = timer.ElapsedMilliseconds;

			outfile.Write("{0} {1} {2}\n", n, e1[n-1], e2[0]);
		}
		outfile.Close();
	}	
}

