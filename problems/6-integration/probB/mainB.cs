using static System.Console;
using static quad;
using static System.Math;
using System;

class mainB {
	static void Main() {
		Write("\nProblem B:\n");
	
		// Define the function:
		double delta = 1e-3;
		// double eps = 1e-5;
		double eps = 0;

		Func<double, double> f1 = (x) => 1/Sqrt(x);
		int evals1_O4AT = 0;
		int evals1_CC = 0;
		double Q1_O4AT = integrator.O4AT(f1, 0, 1, delta, eps, ref evals1_O4AT);		
		double Q1_CC = integrator.CC_O4AT(f1, 0, 1, delta, eps, ref evals1_CC);	
		
		int evals1_o8av = 0;
		Func<double, double> f1_o8av = (x) => {evals1_o8av++; return f1(x);};
		double Q1_o8av = quad.o8av(f1_o8av, 0, 1,  delta, eps);	

		double res1 = 2.0;	
	
		Write($"\nIntegrating 1/Sqrt(x), with delta = {delta}, eps = {eps}\n");
		Write("Without any transformation:\n");
		Write($"Result:                            {Q1_O4AT}\n");
		Write($"Analytical result:                 {res1}\n");
		Write($"Deviation:                         {Q1_O4AT - res1}\n");
		Write($"Tolerance (delta + eps*|Q|):       {delta+eps*Abs(Q1_O4AT)}\n");
		Write($"Evalutaions of the function:       {evals1_O4AT}\n");
		
		Write("\nWith the Clenshaw Curtis transformation:\n");
		Write($"Result:                            {Q1_CC}\n");
		Write($"Analytical result:                 {res1}\n");
		Write($"Deviation:                         {Q1_CC - res1}\n");
		Write($"Tolerance (delta + eps*|Q|):       {delta+eps*Abs(Q1_CC)}\n");
		Write($"Evalutaions of the function:       {evals1_CC}\n");
		
		Write("\nIn comparison, the matlib o8av rutine:\n");
		Write($"Deviation:                         {Q1_o8av - res1}\n");
		Write($"Evaluations of the function:       {evals1_o8av}\n");

		// Log(x)/Sqrt(x)
		delta = 1e-4;
		//eps = 1e-4;
		eps = 0;
		Func<double, double> f2 = (x) => Log(x)/Sqrt(x);
		int evals2_O4AT = 0;
		int evals2_CC = 0;
		double Q2_O4AT = integrator.O4AT(f2, 0, 1, delta, eps, ref evals2_O4AT);		
		double Q2_CC = integrator.CC_O4AT(f2, 0, 1, delta, eps, ref evals2_CC);

		int evals2_o8av = 0;
		Func<double, double> f2_o8av = (x) => {evals2_o8av++; return f2(x);};
		double Q2_o8av = quad.o8av(f2_o8av, 0, 1,  delta, eps);	

		double res2 = -4.0;
		Write("--------------------------------------------------------------\n");
		Write($"Integrating Log(x)/Sqrt(x), with delta = {delta}, eps = {eps}\n");
		Write("Without any transformation:\n");
		Write($"Result:                            {Q2_O4AT}\n");
		Write($"Analytical result:                 {res2}\n");
		Write($"Deviation:                         {Q2_O4AT - res2}\n");
		Write($"Tolerance (delta + eps*|Q|):       {delta+eps*Abs(Q2_O4AT)}\n");
		Write($"Evalutaions of the function:       {evals2_O4AT}\n");
		
		Write("\nWith the Clenshaw Curtis transformation:\n");
		Write($"Result:                            {Q2_CC}\n");
		Write($"Analytical result:                 {res2}\n");
		Write($"Deviation:                         {Q2_CC - res2}\n");
		Write($"Tolerance (delta + eps*|Q|):       {delta+eps*Abs(Q2_CC)}\n");
		Write($"Evalutaions of the function:       {evals2_CC}\n");
		Write("\nThe intergral did not work for lower delta and eps, due to the Clenshaw-Curtis transformed integrand evaluating to -infinity in some nodes of the open-rule quadrature.\n");
		Write("\nIn comparison, the matlib o8av rutine:\n");
		Write($"Deviation:                         {Q2_o8av - res2}\n");
		Write($"Evaluations of the function:       {evals2_o8av}\n");
		Write("\nConclusion: The Clenshaw-Curtis Transformation reduces the required amount of function evaluations immensly\n");

		// Now for calculating PI as accurately as possible:
		var outfile = new System.IO.StreamWriter("out.PIAccuracy.txt");
		Func<double, double> f3 = (x) => 4*Sqrt(1-x*x);
		double res3 = PI;
		double eps3 = 0.0;
		
		for(double x = 2.0; x < 15.0; x+=0.1) {
			double delta3 = Pow(10, -x);
			int evals3_O4AT = 0;
			double Q3_O4AT = integrator.O4AT(f3, 0, 1, delta3, eps3, ref evals3_O4AT);
			
			int evals3_CC = 0;
			double Q3_CC = integrator.CC_O4AT(f3, 0, 1, delta3, eps3, ref evals3_CC);	
	
			int evals3_o8av = 0;
			Func<double, double> f3_o8av = (variable) => {evals3_o8av++; return f3(variable);};
			double Q3_o8av = quad.o8av(f3_o8av, 0, 1,  delta3, eps3);	

			outfile.Write("{0} {1} {2} {3} {4} {5} {6}\n", delta3, Abs(Q3_O4AT-res3), Abs(Q3_CC-res3), Abs(Q3_o8av-res3), evals3_O4AT, evals3_CC, evals3_o8av);

		}
		
		outfile.Close();

	}
}
