using static System.Console;
using static System.Math;
using System.Collections.Generic;
using System;

class mainA {

	static void Main() {
		Write("Problem C:\n");
				

		var outfile = new System.IO.StreamWriter("out.probC.txt");

		for(double rMax = 7.5; rMax <= 10; rMax += 0.05) {
			// Error.Write($"rMax = {rMax}\n");
			double epsilon = shootingMethod(rMax);
			outfile.Write("{0} {1}\n", rMax, epsilon);
		}

		outfile.Write("\n\n");

		for(double rMax = 1.0; rMax <= 10.0; rMax += 0.1) {
			// Error.Write($"rMax = {rMax}\n");
			double epsilon = shootingMethod_improved(rMax);
			outfile.Write("{0} {1}\n", rMax, epsilon);
		} 
		outfile.Close();

	}
	
	// Shooting method with the boundry condition:
	// f(r->inf) = r*e^(-r*k), k = Sqrt(-2*epsilon)
	static double shootingMethod_improved(double rMax) {
		// Finding the root of this function will usilize
		// the improved boundry condition:
		Func<vector, vector> M_vector = (v) => {
			double epsilon = v[0];
			double boundry = rMax*Exp(-Sqrt(-2*epsilon)*rMax);
			return new vector(M(epsilon, rMax)-boundry);
		};
		
		// Find the minimum close to -1/2:
		vector x0 = new vector(-1.0/2.4);
		double eps_root = 1e-5;
		vector root = rootFinder.newton(M_vector, x0, epsilon:eps_root);
		
		// Return the found value of epsilon:
		return root[0];

	}
	
	static double shootingMethod(double rMax) {
		// Rewrite the M function to be a vector function:
		// Here also a quick hack to make it dependent on rMax:
		Func<vector, vector> M_vector = (v) => new vector(M(v[0], rMax));
		
		// Find the minimum close to -1/2:
		vector x0 = new vector(-1.0/2.4);
		double eps_root = 1e-5;
		vector root = rootFinder.newton(M_vector, x0, epsilon:eps_root);
		
		// Return the found value of epsilon:
		return root[0];
	}
	
	static double M(double epsilon, double rMax) {
		// M is defined as: F_epsilon(r_max)
		vector ode_y = solveShrod(epsilon, rMax);
 		// The value of M(epsilon) the value of the found wavefunction	
		// at rMax:
		return ode_y[0];
	}
	
	static vector solveShrod(double epsilon, double rMax, List<double> ts=null, List<vector> ys=null) {
		// The initial condition is to second order in the starting r
		// so the r should be smaller than the square root of the 
		// accuracy goal:
		double ode_acc = 1e-6;
		double ode_eps = 1e-6;
		double a = Sqrt(ode_acc/2);
		// Initial vector given from:
		// f(r->0) =  r-r^2
		// => f'(r->0) = 1-2*r
		vector ode_y = new vector(a-a*a, 1-2*a);
		// Now solve the equation using my own Runge Kutta 45 method:
		// Starting step:
		double h = 0.01;
		rkode.rk45(diff_f_maker(epsilon), a, ref ode_y, rMax, h, ode_acc, ode_eps, ts, ys);
		return ode_y;
	}

	static Func<double, vector, vector> diff_f_maker(double epsilon) {
		return (r, f) => new vector(f[1], -2*(epsilon*f[0] + f[0]/r));
	}
}
