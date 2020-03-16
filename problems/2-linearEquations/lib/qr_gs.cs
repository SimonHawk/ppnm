using static cmath;
using static System.Console;

// Class for handling methods related to qr decomposition.
public class qr_gs {
	// Matricies for q and r:
	matrix r;
	matrix q;
	// Properties to acess Q and R
	public matrix Q{get{return q;}}
	public matrix R{get{return r;}}
	
	// Constructor for the class: Calculates q and r with the gram schmitt
	// decomposition.
	public qr_gs(matrix A) {
		qr_gs_decomp(A);
	}

	// Calculates the decomposition of the matrix A and saves the result in
	// q and r:
	public void qr_gs_decomp(matrix A) {
		int n = A.size1;
		int m = A.size2;
		
		// Make a static copy of A, and make the variable A refer to q, 
		// for convinience in the definition here:
		q = A.copy();
		A = q;
		
		r = new matrix(m, m);
		
		for(int i = 0; i < m; i++) {
			// Calculate R_ii:
			r[i, i] = sqrt(coulumnInnerProduct(A, i, A, i));
			// Calculate the i'th column of Q:
			for(int k = 0; k < n; k++) {
				q[k, i] = A[k, i]/r[i, i];
			}
			// Calculate the off-diagonal R's
			for(int j = i+1; j < m; j++) {
				// Calculate R_ij
				r[i, j] = coulumnInnerProduct(q, i, A, j);
				// Recalculate the rest of the A columns:
				for(int k = 0; k < n; k++) {
					A[k, j] = A[k, j] - q[k, i]*r[i, j];
				}
			}
		}
		// After orthogonalization, A is equal to Q:
		q = A;
	}
	
	// Solves the system: QRx = b and returns the results as a vector:
	public vector qr_gs_solve(vector b) {
		// Lets do this in the in place style, just to learn it:
		Write($"Size of Q.transpose(): {Q.size1}x{Q.size2}, size of b: {b.size}\n");
		vector x = Q.transpose()*b;
		backSubstitution(R, x);
		return x;
	}
	
	// Performs in place backsubsitution, solving the vector x with respect
	// to the upper tridiagonal matrix T:
	public void backSubstitution(matrix T, vector x) {
		x[x.size-1] = x[x.size-1]/T[x.size-1, x.size-1];
		// Write($"First we calculate x_{x.size-1} = {x[x.size-1]}\n");
		// Write($"T: {T.size1}x{T.size2}, x: {x.size}\n");
		for(int i = x.size-2; i >= 0; i--) {
			// Write($"Then we calculate x_{i}\n");
			// Write("Backsubs: i={0}\n", i);
			double sum = 0;
			// Write($"Sum for x_{i} = ");
			for(int k = i+1; k <= x.size-1; k++) {
				// Write($"+ {T[i, k]}*{x[k]} ");
				sum += T[i, k]*x[k];
			}
			// Write($" = {sum}\n");
			// Write($"x_{i} = ({x[i]} - {sum})/({T[i, i]})");
			x[i] = (x[i] - sum)/(T[i, i]);
			// Write($" = {x[i]}\n");
		}
	}
	
	public matrix qr_gs_inverse() {
		int size = q.size1;
		matrix inverse = new matrix(q.size1, q.size2);
		vector b_j;
		for(int j = 0; j < q.size2; j++) {
			b_j = qr_gs_solve(unitVector(j));
			for(int i = 0; i < q.size1; i++) {
				inverse[i, j] = b_j[i];
			}
		}
		return inverse;
	}
	
	vector unitVector(int i) {
		int size = q.size1;
		vector e_i = new vector(size);
		for(int j = 0; j < size; j++) {
			// Smart little conditional statement:
			e_i[j] = i==j?1:0;
		}
		return e_i;
	}
	
	// Takes the inner product of two matrix coulumns. The matricies must
	// have the same number of rows!
	public double coulumnInnerProduct(matrix A, int ia, matrix B, int ib) {
		if(A.size1 != B.size1) {
			throw new System.ArgumentException("The two matricies must have the same number of rows!", "matrix");
		}
		double product = 0;
		for(int i = 0; i < A.size1; i++) {
			product += A[i, ia] * B[i, ib];
		}
		return product;
	}

}
