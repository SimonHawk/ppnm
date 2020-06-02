using static System.Math;
using static System.Console;
using System;

public class eigen {

	public static void Main() {
		int n = 10;
		matrix A = matrixHelp.makeRandSymMatrix(n);

		int m = 5;
		
		matrix V = new matrix(n, m);
		matrix T = new matrix(m, m);
		
		lanczos.interations(A, m, V, T);
		

	}

}
