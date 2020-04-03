using static System.Console;
using static System.Math;
using System.Collections.Generic;
using System;

class mainA {
	static void Main() {
		// Make the function for the differential equation: u'' = -u
		Func<double, vector, vector> f = (t, yt) => new vector(yt[1], -yt[0]);
		// Make the starting conditions for a cosine fuction:
		double a = 0;
		vector y1 = new vector(0.0, 1.0);
		vector y2 = new vector(0.0, 1.0);

		// End point and step size:
		double b = 10;
		double h = 0.01;

		// Set the wanted accuracy and epsilon:
		double acc = 0.0001;
		double eps = 0.0001;
	
		List<double> ts1 = new List<double>();
		List<vector> ys1 = new List<vector>();
		List<double> ts2 = new List<double>();
		List<vector> ys2 = new List<vector>();

		int steps12 = rkode.rk12(f, a, ref y1, b, h, acc, eps, ts1, ys1);
		int steps45 = rkode.rk45(f, a, ref y2, b, h, 0.0001, 0.0001, ts2, ys2);
		
		Write($"RK12 done in {steps12} steps, RK45 done in {steps45} steps\n");
		
		// Write the output file:
		var outfile = new System.IO.StreamWriter("out.probA.txt");
		for(int i = 0; i < ts1.Count; i++) {
			outfile.Write("{0} {1} {2}\n", ts1[i], ys1[i][0], ys1[i][1]);
		}
		outfile.Write("\n\n");
		for(int i = 0; i < ts2.Count; i++) {
			outfile.Write("{0} {1} {2}\n", ts2[i], ys2[i][0], ys2[i][1]);
		}
		outfile.Close();

	}
}
