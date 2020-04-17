using static System.Console;
using static System.Math;
using System;

class mainB {
	static void Main() {
		Write("\nProblem B:\n");
	
		// Define the function:
		double delta = 1e-5;
		// double eps = 1e-5;
		double eps = 0;

		Func<double, double> f1 = (x) => 1/Sqrt(x);
		int evals1_O4AT = 0;
		int evals1_CC = 0;
		double Q1_O4AT = integrator.O4AT(f1, 0, 1, delta, eps, ref evals1_O4AT);		
		double Q1_CC = integrator.CC_O4AT(f1, 0, 1, delta, eps, ref evals1_CC);
		Write($"\nIntegrating 1/Sqrt(x), with delta = {delta}, eps = {eps}\n");
		Write($"Without transformation: {Q1_O4AT}, deviation from 2: {Q1_O4AT-2.0}, required evaluations of the integrand: {evals1_O4AT}\n");
		Write($"With Clenshaw Curtis Transformation: {Q1_CC}, deviation from 2: {Q1_CC-2}, required evaluations of the integrand: {evals1_CC}\n");

		// Log(x)/Sqrt(x)
		delta = 1e-4;
		//eps = 1e-4;
		eps = 0;
		Func<double, double> f2 = (x) => Log(x)/Sqrt(x);
		int evals2_O4AT = 0;
		int evals2_CC = 0;
		double Q2_O4AT = integrator.O4AT(f2, 0, 1, delta, eps, ref evals2_O4AT);		
		double Q2_CC = integrator.CC_O4AT(f2, 0, 1, delta, eps, ref evals2_CC);
		Write($"\nIntegrating Log(x)/Sqrt(x), with delta = {delta:e3}, eps = {eps:e3}\n");
		Write($"Without transformation: {Q2_O4AT:e3}, deviation from -4: {Q2_O4AT+4:e3}, required evaluations of the integrand: {evals2_O4AT}\n");
		Write($"With Clenshaw Curtis Transformation: {Q2_CC:e3}, deviation from -4: {Q2_CC+4:e3}, required evaluations of the integrand: {evals2_CC}\n");
		Write("The intergral did not work for lower delta and eps, due to the Clenshaw-Curtis transformed integrand evaluating to -infinity in some nodes of the open-rule quadrature.\n");
		Write("\nConclusion: The Clenshaw-Curtis Transformation reduces the required amount of function evaluations immensly\n");
		/*	
		double step = 1e-16;
		for(double x = 0.0; x < 20*step; x+=step) {
			Write($"log({x}) = {Log(x)}\n");
		}
		*/


	}
}
