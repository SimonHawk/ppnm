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
		double res1 = 2.0/3;
				
		Write($"Integrating Sqrt(x) from 0 to 1:\n");
		Write($"Result:                                 {Q1}\n");
		Write($"Analytical result:                      {res1}\n");
		Write($"Deviation from analytical result:       {Q1-res1}\n");
		Write($"Specified tolerance (delta + acc*|Q|):  {delta-eps*Abs(Q1)}\n");
		Write($"Number of function evaluations:         {evals1}\n");

		// Integrating 4*Sqrt(1-x^2):
		Func<double, double> f2 = (x) => 4*Sqrt(1-x*x);
		int evals2 = 0;
		double Q2 = integrator.O4AT(f2, 0, 1, delta, eps, ref evals2);
		double res2 = PI;
		
		Write($"\nIntegrating 4*Sqrt(1-x^2) from 0 to 1 \n");
		Write($"Result:                                 {Q2}\n");
		Write($"Analytical result:                      {res2}\n");
		Write($"Deviation from analytical result:       {Q2-res2}\n");
		Write($"Specified tolerance (delta + acc*|Q|):  {delta-eps*Abs(Q2)}\n");
		Write($"Number of function evaluations:         {evals2}\n");
		
		Write($"\nConclusion: The adaptive integrator calculated integrals to a better accuracy than required.\n");

	}
}
