using static System.Math;
using static System.Console;

public partial class jacobi {

	public static int values(matrix A, vector e, int nvalues=1, matrix V=null, bool highest=false) {
		int n = A.size1;
		// Initialize the vector e, diagonal of A:
		for(int i = 0; i < n; i++) e[i] = A[i,i];
		// Initialize the matrix V, starts as unity:
		if(V!=null) for(int i = 0; i < V.size1; i++) {
			V[i,i] = 1.0;
			for(int j  = i+1; j < V.size1; j++) {
				V[i, j] = 0.0;
				V[j, i] = 0.0;
			}
		}
		
		// Setup for the sweeps:
		bool changed = false;
		int sweeps = 0;
		int rotations = 0;
		
		for(int p = 0; p < nvalues; p++) do {
			sweeps++;
			changed = false;
			
			// Setup the sweeps:
			for(int q = n-1; q > 0; q--)  {
				rotations++;
				// Calculate the rotation of the diagonal:
				double App = e[p];
				double Aqq = e[q];
				double Apq = A[p,q];
				
				double theta;
				// The following theta will give lowest eigenvalues first
				if(!highest) theta = 0.5*Atan2(2*Apq, Aqq-App);
				// And this will give highest eigenvalues first:
				else theta = 0.5*Atan2(-2*Apq, App-Aqq);
				double c = Cos(theta);
				double s = Sin(theta);
					
				double App1 = c*c*App - 2*s*c*Apq + s*s*Aqq;
				double Aqq1 = s*s*App + 2*s*c*Apq + c*c*Aqq;
				// If the diagonal changes under rotation:
				if(App1 != App || Aqq1 != Aqq) {
					changed = true;
					// Update e, which represents the diagonal, and A[p,q]
					e[p] = App1;
					e[q] = Aqq1;
					A[p, q] = 0.0;
					// Now do the rotations for the rest of the matrix:
					// The "i1" part of the ratation is not
					// needed now as all elements above are 0:
					for(int i2 = p+1; i2 < q; i2++) {
						double Api = A[p,i2];
						double Aiq = A[i2,q];
						A[p,i2] = c*Api - s*Aiq;
						A[i2,q] = s*Api + c*Aiq;
					}
					for(int i3 = q+1; i3 < n; i3++) {
						double Api = A[p,i3];
						double Aqi = A[q,i3];
						A[p,i3] = c*Api - s*Aqi;
						A[q,i3] = s*Api + c*Aqi;
					}
					if(V!=null) for(int i = 0; i < A.size1; i++) {
						double Vip = V[i,p];
						double Viq = V[i,q];
						V[i,p] = c*Vip - s*Viq;
						V[i,q] = s*Vip + c*Viq;
					} // End of V update
				}// End of A update 
			}//End of loop over q and p	
		
		} while(changed); // End of do while loop
		return rotations;
	}// End of cyclic()

}// End of jacobi class


