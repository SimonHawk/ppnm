using static System.Console;
using static System.Math;
using System;

class mainA {
	static void Main() {
		double pInf = double.PositiveInfinity;
		double nInf = double.NegativeInfinity;	

		Write("Problem c:\n");
		Func<double, double> f1 = (x) => Exp(-x*x);
		double delta = 1e-5;
		double eps = 1e-5;
		double res1 = Sqrt(PI);

		Write($"Integrating Exp(-x^2) from -inf to inf:\n");
		testFuncC(f1, nInf, pInf, res1, delta, eps);	
		
		Write("\nIntegrating 1/(x^2+2^2) from 0 to inf:\n");	
		testFuncC((x)=>1/((x*x)+2*2), 0, pInf, PI/4, delta, eps);
	
		Write("\nIntegrating sinc(x) = sin(x)^2/x^2 from -inf to 0:\n");
		Write("Expected result PI/2\n");
		testFuncC((x)=>Sin(x)*Sin(x)/(x*x), nInf,  0, PI/2, delta, eps);
		
		Write("\nWow, o8av is actually a bit above the accuracy goal for this one\n");
		
		/*

		// First integral: Unit gaussian:
		Func<double, double> f1 = (x) => Exp(-x*x);
		double delta = 1e-5;
		double eps = 1e-5;
		int evals1 = 0;
		double Q1 = integrator.O4ATV(f1, nInf, pInf, delta, eps, ref evals1);		
		double res1 = Sqrt(PI);
				
		Write($"Integrating Exp(-x^2) from -inf to inf:\n");
		Write($"Result:                                 {Q1}\n");
		Write($"Analytical result (Sqrt(PI)):           {res1}\n");
		Write($"Deviation from analytical result:       {Q1-res1}\n");
		Write($"Allowed tolerance (delta + acc*|Q|):    {delta-eps*Abs(Q1)}\n");
		Write($"Number of function evaluations:         {evals1}\n");
		
		int evals1_o8av = 0;
		Func<double, double> f1_o8av = (x) => {evals1_o8av++; return f1(x);};		
		double Q1_o8av = quad.o8av(f1_o8av,  nInf, pInf, delta, eps);
		Write("Comparing with o8av:\n");
		Write($"o8av deviation:                         {Q1_o8av-res1}\n");
		Write($"o8av function evaluations:              {evals1_o8av}\n");
		*/
		/*
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
		*/
	}
	
	public static void testFuncC(
		Func<double, double> f,
		double a, 
		double b, 
		double res,  
		double delta,  
		double eps
	) {

		double pInf = double.PositiveInfinity;
		double nInf = double.NegativeInfinity;	

		// First integral: Unit gaussian:
		int evals = 0;
		double Q = integrator.O4ATV(f, a, b, delta, eps, ref evals);		
				
		Write($"Result:                                 {Q}\n");
		Write($"Analytical result):                     {res}\n");
		Write($"Deviation from analytical result:       {Q-res}\n");
		Write($"Allowed tolerance (delta + acc*|Q|):    {delta-eps*Abs(Q)}\n");
		Write($"Number of function evaluations:         {evals}\n");
		
		int evals_o8av = 0;
		Func<double, double> f_o8av = (x) => {evals_o8av++; return f(x);};		
		double Q_o8av = quad.o8av(f_o8av, a, b, delta, eps);
		Write("Comparing with o8av:\n");
		Write($"o8av deviation:                         {Q_o8av-res}\n");
		Write($"o8av function evaluations:              {evals_o8av}\n");
	}
}
