using static cmath;

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
		vector x = Q.transpose()*b;
		backSubstitution(R, x);
		return x;
	}
	
	// Performs in place backsubsitution, solving the vector x with respect
	// to the upper tridiagonal matrix T:
	public void backSubstitution(matrix T, vector x) {
		x[x.size-1] = x[x.size-1]/T[x.size-1, x.size-1];
		for(int i = x.size-1; i > 0; i--) {
			x[i-1] = (x[i-1]-T[i+1, i]*x[i])/(T[i-1, i-1]);
		}
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
