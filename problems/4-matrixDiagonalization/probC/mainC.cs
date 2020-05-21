using static System.Console;
using static System.Math;
using System;
using System.Diagnostics;

class mainA {
	static void Main() {
		Write("\n\nProblem C:\n");
		// Test if the classical one works:
		int n1 = 5;
		matrix A1 = matrixHelp.makeRandSymMatrix(5);
		matrix A1c = A1.copy();
		matrix V1 = new matrix(n1,n1);
		vector e1 = new vector(n1);
		
		Write("Diagonalizing matrix A1 by classical jacobi:\n");
		A1.print("A1 = ");
		
		int rot1 = jacobi.classic(A1, e1, V1);
		
		Write($"Done in {rot1} rotations\n");
		Write($"A after diagonalization:\n");
		A1.print("A1 = ");
		
		Write($"Found eigenvalues: {e1}\n");
		
		Write("A1 in the eigenbasis:\n");
		(V1.transpose()*A1c*V1).print("V1^T * A1 * V1 = ");
				

		// Make a comparison plot:
		var timer = new Stopwatch();
		timer.Start();
		var outfile = new System.IO.StreamWriter("out.dataC.txt");
		int NMin1 = 10;
		int NMax1 = 200;
		int step1 = (NMax1-NMin1)/20;
		for(int n = NMin1; n < NMax1; n+= step1) {
			matrix A = matrixHelp.makeRandSymMatrix(n);
			matrix V = new matrix(n,n);
			vector e = new vector(n);
			timer.Restart();
			int rot = jacobi.classic(A, e, V);
			timer.Stop();
			double time = timer.ElapsedMilliseconds;
			//Error.Write($"Classic time for n = {n}: {time}\n");
			if(n != NMin1) outfile.Write("{0} {1} {2}\n", n, time, rot);
		}
		outfile.Write("\n\n");

		int NMin2 = 10;
		int NMax2 = 200;
		int step2 = (NMax2-NMin2)/20;
		for(int n = NMin2; n < NMax2; n+= step2) {
			matrix A = matrixHelp.makeRandSymMatrix(n);
			matrix V = new matrix(n,n);
			vector e = new vector(n);
			timer.Restart();
			int rot = jacobi.cyclic(A, e, V);
			timer.Stop();
			double time = timer.ElapsedMilliseconds;
			//Error.Write($"Cyclic time for n = {n}: {time}\n");
			if(n != NMin1) outfile.Write("{0} {1} {2}\n", n, time, rot);
		}
		outfile.Close();
		
		Write("\n\nSee C.comparison.svg for a comparison of rotations and time to diagonalizes matricies with the cyclic and classic jacobi method.\n It seems that the classic approach uses less rotations, but the two are about equal in computation time, due to the overhead of having to update the array of highest indexes.\n");
	}	
}
