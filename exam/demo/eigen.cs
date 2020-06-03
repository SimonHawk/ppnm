using static System.Math;
using static System.Console;
using System;

public class eigen {

	public static void Main() {
		//
		Write("Demonstration of finding eigenvalues\n");
		int n = 6;
		matrix A = matrixHelp.makeRandSymMatrix(n);
		matrix A_J = A.copy();

		int m = n;
		
		matrix E = new matrix(n, m);
		vector e = new vector(m);

		lanczos.eigenvalues(A, E, e);
		
		//
		Write($"For a random symmetric matrix of size {n}x{n}\n");
		Write($"The Lanczos algorithm will attempt to produce all {m} eigenvalues.\n");
		
		Write($"The following eigenvalues were found:\n");		

		Write($"{e}\n");
		Write($"\nWith the corresponding eigenvectors:\n");
		E.print();
		
		// Comparison to Cyclic jacobi-		
		matrix E_J = new matrix(n,n);
		vector e_J = new vector(n);
		
		jacobi.cyclic(A_J, e_J, E_J);

		Write("\n\nIn comparison, the cyclic jacobi method find the following:\n");
		Write($"Eigenvectors: \n{e_J}\n");
		Write($"\nAnd corresponding eigenvalues:\n");
		E_J.print();
		
		Write("\nIt is seen for n = m, that the Lanczos and Jacobi methods produce the same eigenvalues and eigenvector (abeit sometimes with a difference of a sign on the vectors).\n");


		Write("\n\n\nSetting m < n enables Makes the behavoir different:\n");
		
		n = 8;
		A = matrixHelp.makeRandSymMatrix(n);
		A_J = A.copy();

		m = 6;
		
		E = new matrix(n, m);
		e = new vector(m);

		lanczos.eigenvalues(A, E, e);
		
		//
		Write($"For a random symmetric matrix of size {n}x{n}\n");
		Write($"The Lanczos algorithm will attempt to produce m = {m} eigenvalues.\n");
		
		Write($"The following eigenvalues were found:\n");		

		Write($"{e}\n");
		Write($"\nWith the corresponding eigenvectors:\n");
		E.print();
		
		// Comparison to Cyclic jacobi-		
		E_J = new matrix(n,n);
		e_J = new vector(n);
		
		jacobi.cyclic(A_J, e_J, E_J);

		Write("\n\nIn comparison, the cyclic jacobi method find the following:\n");
		Write($"Eigenvectors: \n{e_J}\n");
		Write($"\nAnd corresponding eigenvalues:\n");
		E_J.print();
		
		Write("\nThe largest and lowest eigenvectors and corresponding eigenvalues are well approximated by the Lanczos method, but they deviate more than the correct Jacobi method found ones as we get closer to the 'middle' eigenvalues.\n");
		
		Write("\nHowever, this can be useful in compressing large matricies into smalller matricies, conserving the extreme eigenvalues.\n");		

		n = 30;
		m = 10;
		A = matrixHelp.makeRandSymMatrix(n);
		E = new matrix(n, m);
		e = new vector(m);
		lanczos.eigenvalues(A, E, e);
		
		A_J = A.copy();
		E_J = new matrix(n, n);
		e_J = new vector(n);
		jacobi.cyclic(A_J, e_J, E_J);

		Write("\n\nMany eigenvalues:\n");
		Write($"Lanczos\n{e}\n");
		Write($"Jacobi\n{e_J}\n");
		
				

	}

}
