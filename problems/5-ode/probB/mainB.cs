using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System;

class mainB {
	
	static void Main() {
		// Setup basic parameters:
		double N = 7e6; // Population
		double Tr = 10; // Days to recovery	
		double Tc = 4; // Days between contacts, based on infection ratio 2.5
		
		double t0 = 0.0;
		// How many are infected at the start? Lets say 500 people came home from travels with the virus
		double I_start = 500;
		vector y0 = new vector(new double[] {N-I_start, I_start, 0});
		
		// Step size and accuracy goals:
		double tb = 30*4; // 1 month, 31 days.
		double h = 0.01;
		double acc = 1e-5;
		double eps = 1e-5;

		// Prepare variables to save steps:
		var ts = new List<double>();
		var ys = new List<vector>();
		
		// Do the integration:
		int steps = rkode.rk45(makeSIREq(N, Tc, Tr), 0, ref y0, tb, h, acc, eps, ts, ys);
		
		Write($"Done in {steps} steps!\n");
		
		var outfile = new System.IO.StreamWriter("out.probB.txt");

		for(int i = 0; i < ys.Count; i++) {
			outfile.Write("{0} {1} {2} {3}\n", ts[i], ys[i][0], ys[i][1], ys[i][2]);		
		}
		outfile.Close();
	}

	public static Func<double, vector, vector> makeSIREq(double N, double Tc, double Tr) {
			return (x, y) => {
				vector yd = new vector(3);
				yd[0] = -(y[1]*y[0])/(N*Tc);
				yd[1] = (y[1]*y[0])/(N*Tc) - y[1]/Tr;
				yd[2] = y[1]/Tr;
				return yd;
			};
		}
	
}
