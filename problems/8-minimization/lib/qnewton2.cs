using static System.Math;
using static System.Console;
using System;

public class minimization {
	
	public static vector qnewton(
		Func<vector, double> f, //
		vector xstart, 
		double eps,
		ref int steps,
		matrix B=null // the inverse of the hesse matrix
	) {	
		steps += 1;
		int n = xstart.size;
		// Initiate the B matrix if it isn't given:
		if(B == null) {
			B = new matrix(n, n);
			B.set_identity();
		}
		
		// 1: calculate grad_f:
		vector grad_f = gradient(f, xstart);
		// 2: Calculate delta_x:
		vector delta_x = -B*grad_f;

		// 3: Do the backtracking to find the actual step s:
		
		double alpha = 1e-4;
		int invlambda = 1;
		while((invlambda <= 64) && (f(xstart+delta_x/invlambda) >= f(xstart) + alpha*(delta_x/invlambda).dot(grad_f) )) {
			invlambda *= 2;
		}
		vector s = delta_x/invlambda;	
	
		// If lambda becomes too small, reset B:
		if(invlambda > 64) {
			B.set_identity();
		}
		
		// 4: Calculate the error and compare with the accuracy goal:
		// Notes: err = Abs(grad_phi):

		// first the gradient of the next step:
		vector grad_f_s = gradient(f, xstart+s);

		double err = grad_f_s.simpleNorm();
		
		if(steps > 999) {
			Error.Write($"qnewton: Maximum number of steps reached ({steps} steps), terminating minimization.\n");
			return xstart + s;
		} else if(err < eps) {
			return xstart + s;
		} else {
			// 5: If the step was not final, 
			// Do SR1 update and do another step:
			// Calculate y
			vector y = grad_f_s - grad_f;
			// Calculate u:
			vector u =  s - B*y;
			
			if(Abs(u.dot(y)) < 1e-6) {
				return qnewton(f, xstart+s, eps, ref steps, B);
			} else {
				// Calculate delta_B:
				matrix delta_B = u.outer(u)/(u.dot(y));
				// Do another step:
			 	return qnewton(f, xstart+s, eps, ref steps, B+delta_B);	
			}
		}
	}
	
	public static vector gradient(Func<vector, double> f, vector xstart) {
		double dx = 1e-8;
		double fx = f(xstart);	
		vector grad_f = new vector(xstart.size);
		for(int i = 0; i < xstart.size; i++) {
			xstart[i] += dx;
			grad_f[i] = (f(xstart) - fx)/dx;			
			xstart[i] -= dx;
		}		
		return grad_f;
	}
}
