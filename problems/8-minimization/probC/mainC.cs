using static System.Console;
using static System.Math;
using System;


class mainC {
	static void Main() {
		Write("Problem C:\n");

		// Define the Rosenbrock valley function:
		int fcalls1 = 0;
		Func<vector, double> rosenbrock = (v) => {
			fcalls1++;
			return ((1-v[0])*(1-v[0]) + 100*(v[1] - v[0]*v[0])*(v[1] - v[0]*v[0]));
		};
		
		// Define the criteria for the minimization:
		vector xstart1 = new vector(3.0, 3.0);	
		double startingStep = 0.5;
		double eps1 = 1e-8;

		simplex rosSimplex = new simplex(rosenbrock, 2);
		
		// Do the minimization
		int steps1 = rosSimplex.search(xstart1, startingStep, eps1);	
		
		vector ares1 = new vector(1.0, 1.0);
		
		Write($"\nMinimizing the Rosenbrock function using Downhill Simplex:\n");
		Write($"Starting point:           {xstart1}\n");
		Write($"Accuracy goal:            {eps1}\n");
		Write($"Found minimum:            {rosSimplex.minimum}\n");
		Write($"Deviation from expected:  {rosSimplex.minimum-ares1}\n");
		Write($"Minimization steps:       {steps1}\n");
		Write($"Function calls:           {fcalls1}\n");
		


		// Define the Rosenbrock valley function:
		int fcalls2 = 0;
		Func<vector, double> himmelblau = (v) => {
			fcalls2++;
			return ((v[0]*v[0]+v[1]-11)*(v[0]*v[0]+v[1]-11)+(v[0]+v[1]*v[1]-7)*(v[0]+v[1]*v[1]-7));
		};
		// Define the criteria for the minimization:
		vector xstart2 = new vector(3.5, 2.5);	
		double startingStep2 = 0.5;
		double eps2 = 1e-5;
		
		simplex himSimplex = new simplex(himmelblau, 2);
		
		int steps2 = himSimplex.search(xstart2, startingStep2, eps2);
		// Do the minimization
		vector ares2 = new vector(3.0, 2.0);
		
		Write($"\nMinimizing the Himmelblau function using Downhill Simplex:\n");
		Write($"Starting point:           {xstart2}\n");
		Write($"Accuracy goal:            {eps2}\n");
		Write($"Found minimum:            {himSimplex.minimum}\n");
		Write($"Deviation from expected:  {himSimplex.minimum-ares2}\n");
		Write($"Minimization steps:       {steps2}\n");
		Write($"Function calls:           {fcalls2}\n");

		Write("\nComparing with the qnewton method from Problem A, Downhill Simplex seems to use less function calls and achieve better precision.\n");
	}
}
