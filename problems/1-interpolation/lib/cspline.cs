using static System.Console;

public class cspline {
	vector x, y, b, c, d;
	
	public cspline(vector xs, vector ys) {
		x = xs;
		y = ys;
		// Find dx, dy and p
		vector dx = new vector(x.size-1);
		vector dy = new vector(x.size-1);
		vector p = new vector(x.size-1);
	 	for(int i = 0; i < p.size; i++) {
			dx[i] = x[i+1]-x[i];
			dy[i] = y[i+1]-y[i];
			p[i] = dy[i]/dx[i];
		}
		// -- SOLVING FOR b --
		//Write($"D, dx.size: {dx.size}\n");
		vector D = new vector(x.size);
		D[0] = 2;
		D[D.size-1] = 2;
		for(int i = 0; i < D.size-2; i++) {
			// Write($"i: {i}, i+1 = {i+1} <= {dx.size-1}\n");
			D[i+1] = 2*dx[i]/dx[i+1] + 2;
		}

		//Write("Q\n");
		vector Q = new vector(x.size-1);
		Q[0] = 1;
		for(int i = 0; i < Q.size-1; i++) {
			Q[i+1] = dx[i]/dx[i+1];
		}
		
		//Write("B\n");
		vector B = new vector(x.size);
		B[0] = 3*p[0];
		B[B.size-1] = 3*p[B.size-2];
		for(int i = 0; i < B.size-2; i++) {
			// Write($"i: {i}, i+1 = {i+1} <= {dx.size-1}\n");
			B[i+1] = 3*(p[i] + p[i+1]*dx[i]/dx[i+1]);
		}
		
		// D tilde:
		Write("Dt\n");
		vector Dt = new vector(D.size);
		Dt[0] = D[0];
		for(int i = 1; i < Dt.size; i++) {
			Dt[i] = D[i] - Q[i-1]/Dt[i-1];
		}

		// B tilde:
		//Write("Bt\n");
		vector Bt = new vector(B.size);
		Bt[0] = B[0];
		for(int i = 1; i < Bt.size; i++) {
			Bt[i] = B[i] - Bt[i-1]/Dt[i-1];
		}
		
		// Finally, solve for b's:
		//Write("b\n");
		b = new vector(x.size);
		b[b.size-1] = Bt[b.size-1]/Dt[b.size-1];
		for(int i = b.size-2; i >= 0; i--) {
			b[i] = (Bt[i]-Q[i]*b[i+1])/Dt[i];
		}
		
		// Calculate the c's
		//Write("c and d\n");
		c = new vector(x.size-1);
		d = new vector(x.size-1);
		for(int i = 0; i < c.size; i++) {
			c[i] = (-2*b[i] - b[i+1] + 3*p[i])/dx[i];
			d[i] = (b[i] + b[i+1] - 2*p[i])/(dx[i]*dx[i]);
		}
	}
	
	public double spline(double z) {
		int iz  = search.binary_search(x, z);
		double dzxi = z-x[iz];
		return y[iz] + b[iz]*dzxi + c[iz]*dzxi*dzxi + d[iz]*dzxi*dzxi*dzxi;
	}
	
	public double derivative(double z) {
		int iz = search.binary_search(x, z);
		double dzxi = z-x[iz];
		return b[iz] + 2*c[iz]*dzxi + 3*d[iz]*dzxi*dzxi;
	}
	
	public double integral(double z) {
		int iz = search.binary_search(x, z);
		double integral = 0;
		for(int i = 0; i < iz; i++) {
			double dxi = x[i+1] - x[i];
			integral += y[i]*dxi + 1.0/2*b[i]*dxi*dxi + 1.0/3*c[i]*dxi*dxi*dxi + 1.0/4*d[i]*dxi*dxi*dxi*dxi;
		}
		double dxiz = z - x[iz];
		integral += y[iz]*dxiz + 1.0/2*b[iz]*dxiz*dxiz + 1.0/3*c[iz]*dxiz*dxiz*dxiz + 1.0/4*d[iz]*dxiz*dxiz*dxiz*dxiz;
		return integral;
	}
}	

