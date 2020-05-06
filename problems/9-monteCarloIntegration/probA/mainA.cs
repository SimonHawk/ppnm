using static System.Console;
using static System.Math;
using System;

class mainA {

	static void Main() {
		Write("Problem A:\n");
		// Maybe just integrate a 1d function first:
		// sin(x)^2 from 0 to Pi/2 = 1?
		 Func<vector, double> f1 = (v) => Sin(v[0])*Sin(v[0]);
		
		// Set the limits:
		vector a1 = new vector(0.0);
		vector b1 = new vector(2*PI);
		
		// Define the number of points:
		int N1 = 10000;
		
		// Do the integral:
		double expectedRes1 = PI;
		Write("Integrating Sin(x)^2 (From 0 to 2Pi):\n");
		testFunction(f1, a1, b1, N1, expectedRes1);	
	
		// Now for Dimitris function:
		Func<vector, double> f2 = (v) =>
			1/(1-Cos(v[0])*Cos(v[1])*Cos(v[2]))*1/(PI*PI*PI);
		vector a2 = new vector(0.0, 0.0, 0.0);
		vector b2 = new vector(PI, PI, PI);
		int N2 = 100000;
		double expectedRes2 = 1.3932039296856768591842462603255;
		Write("\nIntegrating the tripple integral: (Pi^3(1-Cos(x)Cos(y)Cos(z)))^-1 over x, y and z:\n");
		testFunction(f2, a2, b2, N2, expectedRes2);

	}
	
	public static void testFunction(Func<vector, double> f, vector a, vector b, int N, double res) {
		// Do the integral:
		vector intResult = mcIntegrator.plainmc(f, a, b, N);
		
		Write($"Lower limit:                      {a}\n");
		Write($"Upper limit:                      {b}\n");
		Write($"Number of samples:                {N}\n");
		Write($"Integration result:               {intResult[0]}\n");
		Write($"Integration error:                {intResult[1]}\n");
		Write($"Deviation from analytical result: {intResult[0]-res}\n");
	} 
}

