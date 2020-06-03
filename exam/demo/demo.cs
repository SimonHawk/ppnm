using static System.Math;
using static System.Console;
using System;

public class demo {

	public static void Main() {
		Write("Part 1:\n");
		Write("Demonstrating that the Lanczos iterations produces the right V and T matrix:\n");

		int n = 6;
		matrix A = matrixHelp.makeRandSymMatrix(6);
		Write($"Here for n = {6}, m = n\n");
		string fmt = ":F4";
		A.print("The randomly generated symmetric matrix: A = ", fmt);
		
		int m = n;
		matrix V = new matrix(n, m);
		matrix T = new matrix(m, m);
		
		lanczos.iterations(A, V, T);
		
		T.print("The found tridiagonal matrix T:", fmt);
		V.print("The found orthogonal matrix V:", fmt);
	
		Write("\nIf V is orthogonal, then V*V^T = I:\n");
		(V*V.T).print("V*V^T = ", fmt);		

		Write("\nAs n = m here, so V*T*V^T should give A:\n");
		(V*T*V.T-A).print("V*T*V^T - A = ", fmt);

	
	}

}
