using static System.Console;
using static System.Math;
using System;

class mainA {

	static void Main() {
		Write("Problem A:\n");
		
		// Try to find root of sin^2 function, which should be pi.
		Func<vector, vector> sin2 = (x) => new vector((Sin(x[0])*Sin(x[0])));
		// Starting x just a bit below pi:
		vector x0 = new vector(3.0);
		// Call the root finder using the standard epsilon and dx:
		double epsilon = 1e-5;
		vector root1 = rootFinder.newton(sin2, x0, epsilon:epsilon);
		double res1 = PI;

		Write($"\nLooking for root of Sin(x)^2 close to {x0[0]:f3}:\n");
		Write($"Accuracy goal:                    {epsilon}\n");
		Write($"Analytical root:                  {res1}\n");
		Write($"Found root:                       {root1[0]}\n");
		Write($"Deviation from analytical result: {root1[0]-res1}\n");
		Write($"Value of function in found root:  {sin2(root1)[0]}\n");

		// Rosenbock function:
		// (1-x)^2 + 100*(y-x^2)^2
		// Define derivative:
		Func<vector, vector> f2 = (v) => new vector(-2*(1-v[0]) + 100*2*(v[1]-v[0]*v[0])*2*v[0], 100*2*(v[1]-v[0]*v[0]));
		// Analytical minimum at (1,1):
		vector x0_2 = new vector(1.5, 0.7);
		double epsilon2 = 1e-5;
		vector root2 = rootFinder.newton(f2, x0_2, epsilon:epsilon2);
		vector res2 = new vector(1.0, 1.0);
		
		Write($"\nLooking for extremum of Rosenbrock function, starting at {x0_2.toString()}:\n");
		Write($"Accuracy goal:                           {epsilon2}\n");
		Write($"Analytical extremum:                     {res2.toString()}\n");
		Write($"Found extremum:                          {root2.toString()}\n");
		Write($"Deviation from analytical result:        {(root2-res2).toString()}\n");
		Write($"Norm of derivative of Rosenbrock function at root:\n");
		Write($"                                         {f2(root2).norm()}\n");

		Write($"\nEven though the method seems to overshoot the accuracy goal, debugging shows that the second-to-last newton step did not fulfill accuracy goal. The newton method simply gets very accurate when approaching the minimum.\n");

	}

}
