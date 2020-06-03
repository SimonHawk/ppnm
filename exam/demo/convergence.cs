using static System.Math;
using static System.Console;
using System;

public class convergence {

	public static void Main() {
		int n = 50;
		
		matrix A = matrixHelp.makeRandSymMatrix(n);
	
		// Using m = n ensures that all the right eigenvalues are found
		matrix E_full = new matrix(n, n);
		vector e_full = new vector(n);
		lanczos.eigenvalues(A, E_full, e_full);
		
		// Setup the output file:
		var outfile = new System.IO.StreamWriter("data.convergence.txt");

		int mMax = 50;
		for(int m = 1; m < mMax; m++) {
			matrix A_m = A.copy();
			matrix E_m = new matrix(n, m);
			vector e_m = new vector(m);
			
			lanczos.eigenvalues(A_m, E_m, e_m);
		
			Func<int, double> e = (i) => {
				if(i < m) {return e_m[i]-e_full[i];}
				else {return Double.NaN;}
			};
			
			outfile.Write("{0} {1} {2} {3} {4} {5}\n", m, e(0), e(1), e(2), e(3), e(4));
			
		}

		outfile.Write("\n\n");
		
		for(int m = 1; m < mMax; m++) {
			matrix A_m = A.copy();
			matrix E_m = new matrix(n, m);
			vector e_m = new vector(m);
			
			lanczos.eigenvalues(A_m, E_m, e_m);
		
			Func<int, double> e = (i) => {
				if(i < m) {
					return Abs(e_m[(e_m.size-1) - i]-e_full[e_full.size-1 - i]);}
				else {return Double.NaN;}
			};
			
			outfile.Write("{0} {1} {2} {3} {4} {5}\n", m, e(0), e(1), e(2), e(3), e(4));
			
		}
		outfile.Write("\n\n");		

		// I will now run the code once more, because it is fast and it
		// it easier to make a pretty data file.		

		for(int m = 1; m < mMax; m++) {
			matrix A_m = A.copy();
			matrix E_m = new matrix(n, m);
			vector e_m = new vector(m);
			
			lanczos.eigenvalues(A_m, E_m, e_m);
			
			// The divergence of the eigenvectors from the "exact" result.
			// here, modelled as the norm of the difference of the two.		
			Func<int, double> E_div = (i) => { if(i < m) {
				double norm1 = (E_m[i]-E_full[i]).norm();
				double norm2 = (E_m[i]+E_full[i]).norm();
				return (norm1 < norm2)?norm1:norm2;
				} else return Double.NaN;
			};

			outfile.Write("{0} {1} {2} {3} {4} {5}\n", m, E_div(0), E_div(1), E_div(2), E_div(3), E_div(4));

		}
		
		outfile.Write("\n\n");
		for(int m = 1; m < mMax; m++) {
			matrix A_m = A.copy();
			matrix E_m = new matrix(n, m);
			vector e_m = new vector(m);
			
			lanczos.eigenvalues(A_m, E_m, e_m);
			
			// The divergence of the eigenvectors from the "exact" result.
			// here, modelled as the norm of the difference of the two.		
			Func<int, double> E_div = (i) => { if(i < m) {
				double norm1 = (E_m[E_m.size2-1-i]-E_full[E_full.size2-1-i]).norm();
				double norm2 = (E_m[E_m.size2-1-i]+E_full[E_full.size2-1-i]).norm();
				return (norm1 < norm2)?norm1:norm2;
				} else return Double.NaN;
			};

			outfile.Write("{0} {1} {2} {3} {4} {5}\n", m, E_div(0), E_div(1), E_div(2), E_div(3), E_div(4));

		}
		outfile.Close();
		
		Write("Testing the convergence of the eigenvalues and vectors.\n\n");
		Write($"Using the Lanczos algorithm on the same {n}x{n} random symmetric matrix,\n");
		Write($"to find eigenvalues and eigenvectors for m = {1} to {mMax} iterations.\n");
		Write($"\nThese are compared to the ones found for m = n = {n}, which were\n");
		Write($"previously shown to be the true values.\n");
		Write("\nFor the eigenvectors, the sign is arbitrary, which was taken into account.\n");
		Write("\nIt appears for m going to n, both the lowest and highest eigenvalues and\n eigenvectors go towards the correct ones.\n");
		Write("However, in all cases, the best convergence seems to be for the single\nhighest eigenvalue.\n");

	}


}


