using static System.Console;
using static System.Math;
using System.Collections.Generic;
using System;

class mainA {

	static void Main() {
		Write("Problem B:\n");
		
		// Calculate the energy:
		double epsilon = shootingMethod();
		double res = -1.0/2;		

		Write($"Looking for the root of M(epsilon) close to {-1.0/2.3}:\n");
		Write($"Accuracy goal:                    {1e-6}\n");
		Write($"Analytical root:                  {res}\n");
		Write($"Found root:                       {epsilon}\n");
		Write($"Deviation from analytical result: {epsilon-res}\n");
		Write($"Value of function in found root:  {M(epsilon)}\n");


		// Export the solution to the schrodinger equation:
		List<double> ts = new List<double>();
		List<vector> ys = new List<vector>();
		solveShrod(epsilon, 8.0, ts, ys);

		var outfile = new System.IO.StreamWriter("out.probB.txt");
		for(int i = 0; i < ts.Count; i++) {
			outfile.Write("{0} {1} {2}\n", ts[i], ys[i][0], ys[i][1]);
		}
		// Seperate two data sets in a single data file:
		outfile.Write("\n\n");
		for(double r = 0; r <= 8.0; r += 0.05) {
			outfile.Write("{0} {1}\n", r, r*Exp(-r));
		}
		outfile.Close();
	}
	
	static double shootingMethod() {
		// Rewrite the M function to be a vector function:
		Func<vector, vector> M_vector = (v) => new vector(M(v[0]));
		
		// Find the minimum close to -1/2:
		vector x0 = new vector(-1.0/2);
		double eps_root = 1e-6;
		vector root = rootFinder.newton(M_vector, x0, epsilon:eps_root);
		
		// Return the found value of epsilon:
		return root[0];
	}
	
	static double M(double epsilon) {
		// M is defined as: F_epsilon(r_max)
		double rMax = 8.0;
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
