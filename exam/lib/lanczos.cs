using static System.Math;
using static System.Console;
using System;

public class lanczos {

	// Wrapper method for giving the matrix A explicitly
	public static void iterations(
		matrix A, // The function v -> A*v
		matrix V, // The n x m matrix to contain V
		matrix T  // The m x m matrix to contain the tridiagonal T matrix
	) {
		Func<vector, vector> applyA = (v) => A*v;
		iterations(applyA, V, T);
	}

	// Base method where the matrix A does not need to be explicitly
	// accecible, just that A*v can be calculated for arbitrary vector v.
	public static void iterations(
		Func<vector, vector> applyA, // The function v -> A*v
		matrix V, // The n x m matrix to contain V
		matrix T  // The m x m matrix to contain the tridiagonal T matrix
	) {
		// A is a n x n matrix:
		int n = V.size1;
		int m = T.size1;
		// Make the starting vector:
		vector v0 = matrixHelp.makeRandUnitVector(n);

		// Do the first itteration:
		vector w0prime = applyA(v0);
		double alpha0 = w0prime.dot(v0);
		vector w0 = w0prime - alpha0*v0;

		// Initialize T and V:
		T[0,0] = alpha0;
		for(int i = 0; i < n; i++) V[i, 0] = v0[i];

		// Save the variables of the loop that will be overwritten.		
		// (Note: This is just new names, so w0 and v0 will be overwritten
		// in the loop, but they are no longer needed.)
		vector wprev = w0;
		vector vprev = v0;

		// Do the following itterations:
		for(int j = 1; j < m; j++) {
			// Calculate betaj:
			double betaj = wprev.norm();

			// Now for the next vector the fast way is:
			// vj = wj-1/(beta_j)
			// But stable way that Dimitri seems to want is:
			// Make an arbitrary unit vector, and make it orthogonal to all
			// the previous vj's by gram schmitt
			// I think I will try to use the above vj and orthogonalize that
			vector vj = wprev/betaj;
			// Now orthogonalize it to all previous vjs:
			vector uj = vj.copy();
			for(int i = 0; i < j; i++) {
				// Maybe I can assume that the columns of u are normed? 
				// This operation might be made more efficient...
				uj -= vj.dot(V[i])/(V[i].norm()) * V[i];
			}
			// normalize uj to be sure, then assign it to vj again:
			vj = uj / uj.norm();


			// Make the new wjprime:
			vector wjprime = applyA(vj);

			// Make the alphaj and wj:
			double alphaj = wjprime.dot(vj);
			vector wj = wjprime - alphaj*vj - betaj*vprev;

			// Update the V and T matrix.
			T[j, j] = alphaj;
			T[j, j-1] = betaj;
			T[j-1, j] = betaj;

			V[j] = vj;
			
			// Set up for the next itteration:
			wprev = wj;
			vprev = vj;

		}
	}


	// Find m eigenvalues of the matrix A, by calculating the T and V
	// matricies, finding the eigenvalues of the T matrix by jacobi
	// rotations, and transforming these to the eigenvalues of the A matrix
	public static void eigenvalues(
		matrix A, // Matrix to find eigenvalues for, size n x n
		matrix E, // The n x m matrix to contain the m found eigenvalues
		vector e  // The m entry vector to contain the found eigenvalues
	) {
		int n = A.size1;
		int m = e.size;

		// Initialize the V and T matricies:
		matrix V = new matrix(n, m);
		matrix T = new matrix(m,m);
		
		// Do the Lanczos algorithm:
		iterations(A, V, T);
		
		// Now for the eigenvalues and eigenvector of T:
		// Initialize storage:
		vector e_T = new vector(m);
		matrix E_T = new matrix(m,m);
				
		jacobi.cyclic(T, e_T, E_T);
		
		// Now transform the result based on:
		// The eigenvalues of T are also eigenvalues of A:
		//		e_A[i] = e_T[i] 
		// The corresponding eigenvectors can be found as: 
		//      E_A[i] = V*E_T[i]:
		for(int i = 0; i < m; i++) {
			e[i] = e_T[i];
			E[i] = V*E_T[i];
		}

		// Done!

	}

}


