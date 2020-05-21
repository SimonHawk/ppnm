using static System.Console;
using static System.Math;
using System;
using System.Diagnostics;

class mainA {
	static void Main() {
		// Test that the diagonalization works as intended: 
		Write("Problem B:\n");
		Write("Problem B.1:\n");
		int nMin = 10;
		int nMax = 100;
		Stopwatch timer = new Stopwatch();		
		
		var fitDataFile = new System.IO.StreamWriter("out.dataB1.txt");

		/*
		matrix A = matrixHelp.makeRandSymMatrix(3);
		matrix V = new matrix(3, 3);
		vector e = new vector(3);
		jacobi.jacobi_cyclic(A, e, V);
		*/
		timer.Reset();
		timer.Start();
		timer.Stop();
		for(int i = nMin; i <= nMax; i++) {
			matrix A = matrixHelp.makeRandSymMatrix(i);
			matrix V = new matrix(i, i);
			vector e = new vector(i);
			timer.Reset();
			timer.Start();
			jacobi.cyclic(A, e, V);
			timer.Stop();
			// Calculate the time in milliseconds:
			double time_i = ((double) timer.ElapsedTicks)/(Stopwatch.Frequency/1000);
			// Write($"i: {i}, time_i = {time_i:f10} ms\n");	
			if(i != nMin) fitDataFile.Write("{0} {1:f10}\n", Log(i), Log(time_i));
			timer.Reset();
		}

		fitDataFile.Close();

		// Checking dependence by a fit.
		Write($"\nCheck the dependence by doing a fit to recorded times for 4x4 to {nMax}x{nMax} random matricies:\n");
		data timingDat = new data("out.dataB1.txt", (x)=>0.0005);
		var fitFuns = new Func<double, double>[]{x=>1, x=>x};
		vector c = leastSquares.calculateC(timingDat, fitFuns);		
		Write($"Linear fit to (log(n), log(time)) suggests that the dependence is O(n^{c[1]:f4})\n");	
		Write($"Total function: log(time) = {c[0]} + {c[1]}*log(n)\n");	
		
		fitDataFile = new System.IO.StreamWriter("out.dataB1.txt", true);
		fitDataFile.Write("\n\n");

		for(int i = 4+1; i < nMax+1; i++) {
			fitDataFile.Write("{0} {1}\n", Log(i), c[0]+c[1]*Log(i));
		}
		
		fitDataFile.Close();		

		probB3();
		probB5();
	}	
	
	// Comparison of time and rotations needed to calculate the lowest
	// eigenvalue for the cyclic and single value method.
	static void probB3() {
		var timer = new Stopwatch();
		timer.Start();
		var outfile = new System.IO.StreamWriter("out.dataB3.txt");
		int NMin1 = 100;
		int NMax1 = 200;
		int step1 = (NMax1-NMin1)/20;
		for(int n = NMin1; n < NMax1; n+= step1) {
			matrix A = matrixHelp.makeRandSymMatrix(n);
			matrix V = new matrix(n,n);
			vector e = new vector(n);
			timer.Restart();
			int rot = jacobi.cyclic(A, e, V);
			timer.Stop();
			double time = timer.ElapsedMilliseconds;
			if(n != NMax1) outfile.Write("{0} {1} {2}\n", n, time, rot);
		}
		outfile.Write("\n\n");

		int NMin2 = 150;
		int NMax2 = 250;
		int step2 = (NMax2-NMin2)/20;
		for(int n = NMin2; n < NMax2; n+= step2) {
			matrix A = matrixHelp.makeRandSymMatrix(n);
			matrix V = new matrix(n,n);
			vector e = new vector(n);
			timer.Restart();
			int rot = jacobi.values(A, e, 1, V);
			timer.Stop();
			double time = timer.ElapsedMilliseconds;
			if(n != NMax2) outfile.Write("{0} {1} {2}\n", n, time, rot);
		}
		outfile.Write("\n\n");

		int NMin3 = 50;
		int NMax3 = 150;
		int step3 = (NMax3-NMin3)/20;
		for(int n = NMin3; n <= NMax3; n+= step3) {
			matrix A = matrixHelp.makeRandSymMatrix(n);
			matrix V = new matrix(n,n);
			vector e = new vector(n);
			timer.Restart();
			int rot = jacobi.values(A, e, n, V);
			timer.Stop();
			double time = timer.ElapsedMilliseconds;
			if(n != NMax3) outfile.Write("{0} {1} {2}\n", n, time, rot);
		}
		outfile.Close();

	}	

	// Comparison of time and rotations needed to calculate the lowest
	// eigenvalue for the cyclic and single value method.
	static void probB5() {
		Write("\n\nProblem B.5:\n");
		int n = 7;
		matrix A1 = matrixHelp.makeRandSymMatrix(n);
		matrix A2 = A1.copy();

		Write("Finding the largest eigenvalue of the matrix:\n");
		A1.print();

		matrix V1 = new matrix(n,n);
		matrix V2 = new matrix(n,n);
		
		vector e1 = new vector(n);
		vector e2 = new vector(n);

		int rot1 = jacobi.cyclic(A1, e1, V1);

		int rot2 = jacobi.values(A2, e2, 1, V2, true);

		Write("\n");
		Write($"Largest found by cyclic jacobi:       {e1[n-1]}\n");
		Write($"Largest found by single row jacobi:   {e2[0]}\n");
		
		Write("\nThe single row method was changed by changing the calculation of the rotation angle theta to: 0.5*Atan2(-Apq, App-Aqq)\n");
		
	}	
}

