using static cmath;
using static System.Console;

// Implementation of QR decomposition based on the modified Gramm-Schmitt
// method:
// This only needs to implement the public void decomp(matrix A) method
// not implemented in the abstract parent class qr_abstract.
public class qr_gs : qr_abstract {
	// For the Gram-Schmitt method it makes sense to store the Q and R
	// matricies explicitly.
	matrix r;
	matrix q;

	public matrix Q{get{return q;}}
	public matrix R{get{return r;}}	

	// Constructor simply calling the constructor of the parent class,
	// qr_abstract:
	public qr_gs(matrix A) : base(A) { }
	
	// Calculates the decomposition of the matrix A and saves the result in
	// q and r:
	override public void decomp(matrix A) {
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
	
	override public vector solve(vector b) {
		vector x = q.transpose()*b;
		backSubstitution(r, x);
		return x;
	}	


}
