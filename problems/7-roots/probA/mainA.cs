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
		vector root1 = rootFinder.newton(sin2, x0, epsilon:1e-7);
		double res1 = PI;

		Write("Looking for root of Sin(x)^2 close to {x0[0]}:\n");
		Write($"Analytical root:                  {res1}\n");
		Write($"Found root:                       {root1[0]}\n");
		Write($"Deviation from analytical result: {root1[0]-res1}\n");
		Write($"Value of function in found root:  {sin2(root1)[0]}\n");

	}

}
