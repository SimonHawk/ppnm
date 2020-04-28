using static System.Math;
using static System.Console;
using System;

public class minimization {
	
	public static vector qnewton(
		Func<vector, double> f, //
		vector xstart, 
		double eps,
		matrix B=null // the inverse of the hesse matrix
	) {
		Error.Write("\nOne call to qnewton!\n");
		Error.Write($"Used xstart: {xstart}!\n");
		double dx = 1e-8;
		int n = xstart.size;
		// Initiate the B matrix if it isn't given:
		if(B == null) {
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
			vector x = xstart.copy();
			x[i] += dx;
			grad_f[i] = (f(x) - f(xstart))/dx;			
		}		
		Error.Write($"Found grad_f: {grad_f}\n");

		// 2: Calculate delta_x:
		vector delta_x = -B*grad_f;
		Error.Write($"Found delta_x: {delta_x}\n");

		// 3: Do the backtracking to find the actual step s:
		
		double alpha = 1e-4;
		int invlambda = 1;
		// TODO: This seems okay, but im not sure.
		while((invlambda <= 64) && (f(xstart+delta_x/invlambda) >= f(xstart) + alpha*(delta_x/invlambda).dot(grad_f) )) {
			invlambda *= 2;
		}
		vector s = delta_x.copy()/invlambda;	
	
		// If lambda becomes too small, reset B:
		if(invlambda > 64) {
			for(int i = 0; i < n; i++) {
				for(int j = 0; j < n; j++) {
					if(i==j) B[i, j] = 1;
					else B[i, j] = 0;
				}
			}
		}
		
		
		// 4: Calculate the error and compare with the accuracy goal:
		// Notes: err = Abs(grad_phi):

		// first the gradient of the next step:
		Error.Write($"xstart + s: {xstart+s}\n");
		Error.Write($"dx:         {dx}\n");
		vector grad_f_s = new vector(n);
		for(int i = 0; i < n; i++) {
			Error.Write("Is this loop running?\n");
			vector x = xstart.copy()+s.copy();
			x[i] += dx;
			Error.Write($"{1}'th entry: f(x) = {f(x)}, f(xstart+s) = {f(xstart + s)} \n");
			grad_f_s[i] = (f(x) - f(xstart+s))/dx;			
		}
		Error.Write($"Found grad_f_s: {grad_f_s}\n");

		double err = grad_f_s.simpleNorm();
		Error.Write($"Found error: {err}\n");
		Error.Write($"Found s: {s}\n");
		
		/*
		Error.Write("ABORTING FOR DEBUGGNING!");
		return xstart+s;
		*/		

		if(err < eps) {
			return xstart + s;
		} else {
			// 5: If the step was not final, update the B matrix and 
			// do another step:
			

			// Calculate y
			vector y = grad_f_s - grad_f;
			
			// If s.dot(y) is too small, update is dangerous, don't
			// do it:
			if(Abs(s.dot(y)) < 1e-6) {	
				return qnewton(f, xstart+s, eps, B);
			} else {
				// Calculate c:
				vector c = (s - B*y)/(s.dot(y));
				// Calculate delta_B:
				matrix delta_B = c.outer(s);
				// Do another step:
			 	return qnewton(f, xstart+s, eps, B+delta_B);	
			}
		}
	}
}
