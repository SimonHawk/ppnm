using static System.Console;
using static System.Math;
using System;


class mainA {
	static void Main() {
		Write("Problem A:\n");

		// Define the Rosenbrock valley function:
		Func<vector, double> cos = 
			(v) => Cos(v[0]);
		
		// Define the criteria for the minimization:
		vector xstart0 = new vector(3.0);	
		double eps0 = 1e-10;
		// Do the minimization
		vector xres0 = minimization.qnewton(cos, xstart0, eps0);
		
		Write($"\nMinimizing the cos function:\n");
		Write($"Starting point:           {xstart0}\n");
		Write($"Accuracy goal:            {eps0}\n");
		Write($"Found minimum:            {xres0}\n");
		Write($"Deviation from expected:  {xres0[0]-PI}\n");
		
		// Define the Rosenbrock valley function:
		Func<vector, double> rosenbrock = 
			(v) => ((1-v[0])*(1-v[0]) + 100*(v[1] - v[0]*v[0])*(v[1] - v[0]*v[0]))/500;
		
		// Define the criteria for the minimization:
		vector xstart1 = new vector(1.1, 1.1);	
		double eps1 = 1e-8;
		// Do the minimization
		vector xres1 = minimization.qnewton(rosenbrock, xstart1, eps1);
		vector ares1 = new vector(1.0, 1.0);
		
		Write($"\nMinimizing the Rosenbrock function:\n");
		Write($"Starting point:           {xstart1}\n");
		Write($"Accuracy goal:            {eps1}\n");
		Write($"Found minimum:            {xres1}\n");
		Write($"Deviation from expected:  {xres1-ares1}\n");



		// Define the Rosenbrock valley function:
		Func<vector, double> himmelblau = 
			(v) => ((v[0]*v[0]+v[1]-11)*(v[0]*v[0]+v[1]-11)+(v[0]+v[1]*v[1]-7)*(v[0]+v[1]*v[1]-7))/500;

		// Define the criteria for the minimization:
		vector xstart2 = new vector(3.5, 2.5);	
		double eps2 = 1e-5;
		// Do the minimization
		vector xres2 = minimization.qnewton(himmelblau, xstart2, eps2);
		vector ares2 = new vector(3.0, 2.0);
		
		Write($"\nMinimizing the Himmelblau function:\n");
		Write($"Starting point:           {xstart2}\n");
		Write($"Accuracy goal:            {eps2}\n");
		Write($"Found minimum:            {xres2}\n");
		Write($"Deviation from expected:  {xres2-ares2}\n");
	}
}
