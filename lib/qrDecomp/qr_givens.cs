using static cmath;
using static System.Console;
using static System.Math;

// Implementation of QR decomposition using Givens rotations.
// This only needs to implement the public void decomp(matrix A) method
// not implemented in the abstract parent class qr_abstract.
public class qr_givens : qr_abstract {
	matrix _G;
	public matrix G{get{return _G;}}
	// Constructor simply calling the constructor of the parent class,
	// qr_abstract:
	
	public qr_givens(matrix A) : base(A) {}
	
	// Decomposes matrix A, in this case saving the the upper triangular 
	// matrix R and the sequence of givens rotations in the same matrix
	// G.
	override public void decomp(matrix A) {
		_G = A.copy();
		double theta_pq;
		double c;
		double s;
		double A_pj2;
		double A_qj2;
		// Itterate over the columns first:
		for(int j = 0; j < _G.size2; j++) {
			// Itterate over all rows below the diagonal:
			for(int i = j+1; i < _G.size1; i++) {
				theta_pq = Atan2(_G[i, j], _G[j, j]);
				// Write($"({i}, {j}): theta = Atan2({_G[i, j]}, {_G[j, j]}) =  {theta_pq}\n");
				c = cos(theta_pq);
				s = sin(theta_pq);
				// Recalculate the relevant rows: 
				// Needs to start from j, as the angles theta are saved in
				// the matrix instead of the 0's that are normally there.
				for(int j2 = j; j2 < _G.size2; j2++) {
					A_pj2 = c*_G[j, j2] + s*_G[i, j2];
					A_qj2 = -s*_G[j, j2] + c*_G[i, j2];
					// Write($"-s*{_G[j, j2]} + c*{_G[i, j2]} = {A_qj2}\n");
					_G[j, j2] = A_pj2;
					_G[i, j2] = A_qj2;	
				}	
				_G[i, j] = theta_pq;
				// Write($"theta_({i}, {j}) = {theta_pq}\n");
				// _G.print($"G = ");
				
			}
		}
	}	
	
	override public vector solve(vector b) {
		// Apply all of the Givens rotations specified by the thethas
		// saved in the matrix G to the vector b. Then solve the system
		// by backsubstitution with the upper diagonal part of the matrix.
		vector x = applyQT(b);
		backSubstitution(_G, x);
		return x;
	}
	
	// In place application of the operation Q^(T) using the givens angles
	// stored in _G on the vector x.
	public vector applyQT(vector b) {
		vector x = b.copy();
		double theta;
		double c;
		double s;
		double x_p;
		double x_q;
		// Two loops to itterate over all theta:
		for(int j = 0; j < _G.size2; j++) {
			for(int i = j+1; i < _G.size1; i++) {
				theta = _G[i, j];
				c = cos(theta);
				s = sin(theta);
				
				x_p	= c*x[j] + s*x[i];
				x_q = -s*x[j] + c*x[i];
			
				x[j] = x_p;
				x[i] = x_q;
			}
		}
		return x;
	}
	/*
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
	*/
	
	/*	
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
	*/
}
