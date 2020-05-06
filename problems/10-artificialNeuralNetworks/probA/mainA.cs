using static System.Console;
using static System.Math;
using System;

class mainA {
	
	static void Main() {
		// Define the activation function as a gaussian:
		Func<double, double> gaussian = (x) => Exp(-x*x);
		// Make a 10 node network:
		int n = 10;
		ann gaussian10 = new ann(n, gaussian);
		
		Write($"Test = {gaussian10.feedforward(1.0)}\n");

		// Quick and dity test on sine funtion:
		double xmin = 0;
		double xmax = 2*PI;
		int NPoints = 10;
		
		vector xs = new vector(NPoints);
		vector ys = new vector(NPoints);
				
		for(int i = 0; i < NPoints; i++) {
			double x = (xmax-xmin)/(NPoints-1) * i;
			xs[i] = x;
			ys[i] = Sin(x);	
		}
		
		Write($"xs = {xs}\n");
		Write($"ys = {ys}\n");

	}

}

