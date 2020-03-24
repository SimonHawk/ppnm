using static System.Math;
using static System.Console;

public class jacobi {
	// Apply a single jacobi rotation to the matrix A in place:
	// Returns true of the diagonal changes:
	public static bool jacobiRotation(int p, int q, matrix A, matrix V) {
		// calculate theta and cos and sin:
		double theta = 0.5*Atan2(2*A[p,q], A[q,q] - A[p,p]);
		double c = Cos(theta);
		double s = Sin(theta);

		// Update the diagonal:
		double App = A[p,p];
		double Aqq = A[q,q];	
		double Apq = A[p,q];
		A[p,p] = c*c*App - 2*s*c*Apq + s*s*Aqq;
		A[q,q] = s*s*App + 2*s*c*Apq + c*c*Aqq;	
		A[p,q] = 0;
		bool changed = (App != A[p,p] || Aqq != A[q,q]);
		// The elements don't mix if you split it up in 3 parts:
		for(int i1 = 0; i1 < p; i1++) {
			double Aip = A[i1, p];
			double Aiq = A[i1, q];
			A[i1, p] = c*Aip-s*Aiq;
			A[i1, q] = s*Aip+c*Aiq;
		}
		for(int i2 = p+1; i2 < q; i2++) {
			double Api = A[p, i2];
			double Aiq = A[i2, q];
			// Note: Remember that A is symmetircal: So A[i, j] = A[j, i]:
			A[p, i2] = c*Api - s*Aiq;
			A[i2, q] = s*Api + c*Aiq;
		}
		for(int i3 = q+1; i3 < A.size1; i3++) {
			double Api = A[p,i3];
			double Aqi = A[q,i3];
			A[p, i3] = c*Api - s*Aqi;
			A[q, i3] = s*Api + c*Aqi;
		}
		for(int i = 0; i < A.size1; i++) {
			double Vip = V[i,p];
			double Viq = V[i,q];
			V[i,p] = c*Vip - s*Viq;
			V[i,q] = s*Vip + c*Viq;
		}
		return changed;
	}
	
	public static int jacobi_cyclic(matrix A, vector e, matrix V) {
		// First initiallize the and matrix V:
		for(int i = 0; i < V.size1; i++) {
			V[i, i] = 1.0;
			for(int j = i+1; j < V.size1; j++) {
				V[i, j] = 0.0;
				V[j, i] = 0.0;
			}
		}	
		// Do the sweeps:
		bool changed = true;
		int sweeps = 0;
		do {
			changed = false;
			sweeps += 1;
			// Write("\n");
			for(int p = 0; p < A.size1; p++) {
				for(int q = p+1; q < A.size1; q++) {
					// If the jacobiRotation returns false, that is if the
					// diagonal of A does not change, stop the while loop
					// by setting changed to false:
					bool pqChanged = jacobiRotation(p, q, A, V);
					// Write($"Sweep {sweeps}: For {p},{q}: pqChanged = {pqChanged}\n");
					if(pqChanged) changed = true;
				}
			}
			// Write($"After sweep {sweeps}, changed = {changed}\n");
		} while(changed);
		// Udate the eigenvalues:		
		for(int i = 0; i < A.size1; i++) {
			e[i] = A[i, i];
		}
		return sweeps;
	}
	
}
