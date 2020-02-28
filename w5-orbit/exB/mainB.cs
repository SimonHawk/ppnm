using System;
using static System.Console;
using static System.Math;
using System.Collections;
using System.Collections.Generic;

class main {
	static int Main(string[] args) {
		// Default values:
		double eps = 0;
		double xa = 0;
		vector ya = new vector(1, 0);
		double xb = 4*PI;
		
		// Update values with arguments based on input arguments
		foreach(string arg in args) {
			// Careful with split: needs to be in single quotes to be
			// a char.
			string[] ws = arg.Split('=');
			switch(ws[0]) {
				case "eps": eps = double.Parse(ws[1]); break;
				case "xa": xa = double.Parse(ws[1]); break;
				case "y0": ya[0] = double.Parse(ws[1]); break;
				case "y1": ya[1] = double.Parse(ws[1]); break;
				case "xb": xb = double.Parse(ws[1]); break;
				default: break;
			}
		}

		solveOrbit(eps, xa, ya, xb);
		return 0;
	}

	static int solveOrbit(double eps, double xa, vector ya, double xb) {
		// Define the ODE function:
		// y0' = y1
		// y1' = 1 + eps*y0*y0 - y0		
		Func<double, vector, vector> planetEQM = delegate(double x, vector y) {
			return new vector(y[1], 1 + eps*y[0]*y[0] - y[0]);
		};
		
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();
		
		vector yb = ode.rk23(planetEQM, xa, ya, xb, xlist:xs, ylist:ys);
		for(int i = 0; i < xs.Count; i++) {
			Write("{0:f16} {1:f16}\n", xs[i], ys[i][0]);
		}

		return 0;
	}
}
