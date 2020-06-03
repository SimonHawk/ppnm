using static System.Math;
using static System.Console;

public partial class jacobi {

	public static int classic(matrix A, vector e, matrix V=null) {
		//Error.Write($"A = \n{A}\n");
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
	
		// Construct array of highest indexes for each row 
		// (Last row is never needed, as we only work on upper diagonal)
		int[] hiIdx = new int[n-1];
		for(int p = 0; p < n-1; p++) hiIdx[p] = n-1;
		for(int p = 0; p < n-1; p++) for(int q = p+1; q < n; q++) {
			if(Abs(A[p, hiIdx[p]]) < Abs(A[p, q])) hiIdx[p] = q;
		}
		
		//Error.Write("hiIdx = ");
		//for(int p = 0; p < n-1; p++) Error.Write($"  {hiIdx[p]}");
		//Error.Write("\n");
		
		// Setup for the sweeps:
		bool changed = false;
		int rotations = 0;
		do {
			changed = false;
			
			// Setup the sweeps:
			// p are the rows:
			for(int p = 0; p < n-1; p++) {
				// Do rotation on the element in the row that is the
				// highest value:
				int q = hiIdx[p];
				// Error.Write($"(q,p) = ({p},{q})\n");
				// Do the rotation on the (p,q)'th element of A,
				// meanwhile updating the hiIdx array for the affected rows
					
				rotations++;
				// Calculate the rotation of the diagonal:
				double App = e[p];
				double Aqq = e[q];
				double Apq = A[p,q];
				
				double theta = 0.5*Atan2(2*Apq, Aqq-App);
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
					for(int i1  = 0; i1 < p; i1++) {
						double Aip = A[i1,p];
						double Aiq = A[i1,q];
						double Aip2 = c*Aip - s*Aiq;
						double Aiq2 = s*Aip + c*Aiq;
						A[i1,p] = Aip2;
						A[i1,q] = Aiq2;
						// Update the hiIdx array:
						if(Abs(Aip2) > Abs(Aiq2)) {
							if(Abs(Aip2) > Abs(A[i1, hiIdx[i1]])) hiIdx[i1] = p;
						} else {
							if(Abs(Aiq2) > Abs(A[i1, hiIdx[i1]])) hiIdx[i1] = q;
						}
					}
					for(int i2 = p+1; i2 < q; i2++) {
						double Api = A[p,i2];
						double Aiq = A[i2,q];
						double Api2 = c*Api - s*Aiq;
						double Aiq2 = s*Api + c*Aiq;
						A[p,i2] = Api2;
						A[i2,q] = Aiq2;
						// Update the hiIdx array:
						if(Abs(Api2) > Abs(A[p, hiIdx[p]])) hiIdx[p] = i2;
						if(Abs(Aiq) > Abs(A[i2, hiIdx[i2]])) hiIdx[i2] = q;
					}
					for(int i3 = q+1; i3 < n; i3++) {
						double Api = A[p,i3];
						double Aqi = A[q,i3];
						double Api2 = c*Api - s*Aqi;
						double Aqi2 = s*Api + c*Aqi;
						A[p,i3] = Api2;
						A[q,i3] = Aqi2;
						// Update the hiIdx array:
						if(Abs(Api2) > Abs(A[p, hiIdx[p]])) {
							hiIdx[p] = i3;
						}
						if(Abs(Aqi2) > Abs(A[q, hiIdx[q]])) {
							hiIdx[q] = i3;
						}
					}
					if(V!=null) for(int i = 0; i < A.size1; i++) {
						double Vip = V[i,p];
						double Viq = V[i,q];
						V[i,p] = c*Vip - s*Viq;
						V[i,q] = s*Vip + c*Viq;
					} // End of V update
				}// End of A update
				/*
				Error.Write($"Updated A = \n{A}\n");
				Error.Write("hiIdx = ");
				for(int i = 0; i < n-1; i++) Error.Write($"  {hiIdx[i]}");
				Error.Write("\n\n");
				*/


			}//End of loop over q and p	
		} while(changed); // End of do while loop
		return rotations;
	}// End of cyclic()

}// End of jacobi class

