using static System.Math;
using static System.Console;
using System;

public class convergence {

	public static void Main() {
		// Setup the output file:
		var outfile = new System.IO.StreamWriter("data.convergence_size.txt");
		int mMax = 30;

		int[] ns = new int[] {5, 10, 30, 50, 100, 150, 250};
		foreach (int n in ns) {
		
			matrix A = matrixHelp.makeRandSymMatrix(n);
		
			// Using m = n ensures that all the right eigenvalues are found
			matrix E_full = new matrix(n, n);
			vector e_full = new vector(n);
			lanczos.eigenvalues(A, E_full, e_full);
			
			for(int m = 1; (m < n)&&(m <= mMax); m++) {
				matrix A_m = A.copy();
				matrix E_m = new matrix(n, m);
				vector e_m = new vector(m);
				
				lanczos.eigenvalues(A_m, E_m, e_m);
			
				Func<int, double> e = (i) => {
					if(i < m) {
						return Abs(e_m[(e_m.size-1) - i]-e_full[e_full.size-1 - i]);}
					else {return Double.NaN;}
				};

				Func<int, double> E_div = (i) => { if(i < m) {
					double norm1 = (E_m[E_m.size2-1-i]-E_full[E_full.size2-1-i]).norm();
					double norm2 = (E_m[E_m.size2-1-i]+E_full[E_full.size2-1-i]).norm();
					return (norm1 < norm2)?norm1:norm2;
					} else return Double.NaN;
				};
				
				outfile.Write("{0} {1} {2}\n", m, e(0), E_div(0));
				
			}

			outfile.Write("\n\n");
		}	

		outfile.Close();

		

	}


}


