using static System.Math;
using System;
using static System.Console;

public class rootFinder {
	
	
	public static vector newton(
		Func<vector, vector> f, // Vector function to find root of
		vector x, // The starting point
		double epsilon=1e-3, // The accuracy goal
		double dx=1e-7 // finite difference to be used for jacobian
	) {
		//Write("One call to newton()\n");
		vector root;
		
		vector f_x = f(x);		

		// 1: Calculate Jacobian
		// Assume f has the same size as x?
		int n = x.size;
		matrix J = new matrix(n, n);
		for(int k = 0; k < n; k++) {
			vector new_x = x.copy();
			new_x[k] += dx;
			vector diff_f = (f(new_x) - f_x)/dx;
			for(int i = 0; i < n; i++) {
				J[i, k] = diff_f[i];
			}
		}
		
		// 2: Find delta_x by solving J*delta_x=-f
		qr_givens Jsolver = new qr_givens(J);
		vector delta_x = Jsolver.solve(-f_x);

		// 3: Find actual step by backtracking line search
		// This loop is hard to read, might have to change.
		double lambda = 1.0;
		while((lambda >= 1.0/64) && (f(x+lambda*delta_x).norm() > (1-lambda/2)*f_x.norm() )) {
			lambda /= 2;
		}
		x = x + lambda*delta_x;
		// Write($"f(x).norm = {f(x).norm()}\n");

		// 4: Check if we are under tolerance
		//	  a: If under tolerance: Return value
		//    b: If not under tolerance: Recursive call with new x		
		if(delta_x.norm() < dx) {
			Error.Write("Step size {delta_x.norm()} is below finite diffrence dx\nStopping itterations and returning best found root...");
			root = x;
		} else if(f(x).norm() < epsilon) {
			root = x;
		} else {
			root = newton(f, x, epsilon, dx);
		}

		return root;
	}
}
