using static System.Math;
using static System.Console;

class minimization {
	
	public static vector qnewton(
		Func<vector, double> f, //
		vector xstart, 
		double eps,
		matrix B=null // the inverse of the hesse matrix
	) {
		double dx = 1e-8;
		int n = xstart.size;
		// Initiate the B matrix if it isn't given:
		if(B = null) {
			B = new matrix(n, n);
			for(int i = 0; i < n; i++) {
				for(int j = 0; j < n; j++) {
					if(i==j) B[i, j] = 1;
					else B[i, j] = 0;
				}
			}
		}
		
		// 1: calculate grad_f:
		vector grad_f = new vector(n);
		for(int i = 0; i < n; i++) {
			vector x = xstart;
			x[i] += dx;
			grad_f[i] = (f(x) - f(xstart))/dx;			
		}		

		// 2: Calculate delta_x:
		vector delta_x = -B*grad_f;
		
		// 3: Do the backtracking to find the actual step s:
		vector s = delta_x.copy();
		double alpha = 1e-4;
		double lambda = 1.0;
		while((lambda >= 1.0/64) && (f(x+s) >= f(x) + alpha*s.dot(grad_f) )) {
			lamda /= 2;
		}
		
		// 4: Calculate the error and compare with the accuracy goal:
		double err = f(xstart + s) - f(xstart);
		if(err < eps) {
			return xstart + s;
		} else {
			// 5: If the step was not final, update the B matrix and 
			// do another step:
			

			// Calculate y
			// first the gradient of the next step:
			vector grad_f_s = new vector(n);
			for(int i = 0; i < n; i++) {
				vector x = xstart+s;
				x[i] += dx;
				grad_f[i] = (f(x) - f(xstart+s))/dx;			
			}
			vector y = grad_f_s - grad_f;
			
			// If s.dot(y) is too small, update is dangerous, don't
			// do it:
			if(Abs(s.dot(y)) < 1e-6) {	
				return qnewton_step(f, xstart+s, eps, B);
			} else {
				// Calculate c:
				vector c = (s - B*y)/(s.dot(y));
				// Calculate delta_B:
				matrix delta_B = c.outer(s);
				// Do another step:
			 	return qnewton_step(f, xstart+s, eps, B+delta_B);	
			}
		
	}

}
