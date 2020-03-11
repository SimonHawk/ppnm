using static System.Console;

public class qspline {
	// Compiles, but gets runtime exception, try make out.B.txt
	vector x, y, b, c;
	public qspline(vector xs, vector ys) {
		x = xs;
		y = ys;
		b = new vector(xs.size-1);
		c = new vector(xs.size-1);
		vector p = new vector(xs.size-1);
		// Claculate the p's
		//Write($"x.size: {x.size}\n");
		for(int i = 0; i < x.size-1; i++) {
			//Write($"i: {i}\n");
			p[i] = (y[i+1] - y[i])/(x[i+1] - x[i]);
		}
		// first pass of c's:
		c[0] = 0;
		//Write($"c.size: {c.size}\n");
		for(int i  = 0; i < c.size-1; i++) {
			//Write($"i: {i}\n");
			c[i+1] = 1/(x[i+2] - x[i+1])*(p[i+1] - p[i] - c[i]*(x[i+1]-x[i]));
		}
		// second pass of the c's:
		c[c.size-1] = c[c.size-1]/2;	
		//Write($"Second pass\n");
		for(int i = c.size-2; i >= 0; i--) {
			//Write($"i: {i}\n");
			c[i] = 1/(x[i+1]-x[i])*(p[i+1] - p[i] - c[i+1]*(x[i+2] - x[i+1]));
		}
		
		// calculate the b's.
		for(int i = 0; i < b.size; i++) {
			b[i] = p[i] - c[i]*(x[i+1] - x[i]);
		}
		
	}
	
	public double spline(double z) {
		int iz = search.binary_search(x, z);
		/* DEBUG
		Write($"SPLINE: z: {z}, iz: {iz}\n");
		Write($"SPLINE: y.size: {y.size}, b.size: {b.size}, c.size: {c.size}\n");
		*/
		return y[iz] + b[iz]*(z-x[iz]) + c[iz]*(z-x[iz])*(z-x[iz]);
	}

	public double derivative(double z) {
		int iz = search.binary_search(x, z);
		return b[iz] + 2*c[iz]*(z-x[iz]);
	}
	
	public double integral(double z) {
		int iz = search.binary_search(x, z);
		double integral = 0;
		for(int i = 0; i < iz; i++) {
			double delta_xi = x[i+1] - x[i];
			integral += y[i]*delta_xi + 1.0/2*b[i]*delta_xi*delta_xi + 1.0/3*c[i]*delta_xi*delta_xi*delta_xi;
		}
		double delta_xiz = z - x[iz];
		integral += y[iz]*delta_xiz + 1.0/2*b[iz]*delta_xiz*delta_xiz + 1.0/3*c[iz]*delta_xiz*delta_xiz*delta_xiz;

		return integral;		
	}
	
	public void printParams() {
		for(int i = 0; i < b.size; i++) {
			Write("b{0}: {1}\t\t c{0}:{2}\n", i, b[i], c[i]);
		}
	}
}
