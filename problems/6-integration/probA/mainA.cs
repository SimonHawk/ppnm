using static System.Console;
using static System.Math;
using System;

class mainA {
	static void Main() {
		Write("Problem A:\n");
	
		// Define the function:
		Func<double, double> f1 = (x) => Sqrt(x);
		double delta = 1e-5;
		double eps = 1e-5;
		int evals1 = 0;
		double Q1 = integrator.O4AT(f1, 0, 1, delta, eps, ref evals1);		
				
		Write($"Integrating Sqrt(x) from 0 to 1 gives: {Q1}\n");
		Write($"Thereby, the deviation from the result 2/3 is: {Q1-2.0/3}\n");
		Write($"Which is within the tolerance of (delta + acc*|Q|) = {delta + eps*Abs(Q1)}\n");
		Write($"This integration was done by {evals1} evaluations of the function\n");

		// Integrating 4*Sqrt(1-x^2):
		Func<double, double> f2 = (x) => 4*Sqrt(1-x*x);
		int evals2 = 0;
		double Q2 = integrator.O4AT(f2, 0, 1, delta, eps, ref evals2);
		
		Write($"\nIntegrating 4*Sqrt(1-x^2) from 0 to 1 gives: {Q2}\n");
		Write($"Thereby, the deviation from the result PI is: {Q2-PI}\n");
		Write($"Which is within the tolerance of (delta + acc*|Q|) = {delta + eps*Abs(Q2)}\n");
		Write($"This integration was done by {evals2} evaluations of the function\n");

	}
}
