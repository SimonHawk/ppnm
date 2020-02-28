using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

class main {
	static int Main(string[] args) {
		double a = 0;
		double b = 10;
		vector ya = new vector(0, 1);
		foreach(string s in args) {
			string[] ws = s.Split('=');
			if(ws[0] == "a") {
				a = double.Parse(ws[1]);		
			} 
			if (ws[0] == "y0") {
				ya[0] = double.Parse(ws[1]);		
			}
			if (ws[0] == "y1") {
				ya[1] = double.Parse(ws[1]);		
			}
			if (ws[0] == "b") {
				b = double.Parse(ws[1]);		
			}
			
		}
		Func<double, vector, vector> sincos = delegate(double x, vector y) {
			return new vector(y[1], -y[0]);
		};
			
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();

		// Yhis is the call to the solver:
		vector yb = ode.rk23(sincos, a, ya, b, xlist:xs, ylist:ys);

		for(int i = 0; i<xs.Count; i++) {
			WriteLine($"{xs[i]} {ys[i][0]} {ys[i][1]}");
		}		

		Error.Write($"Sin({b}) = {Sin(b)}, Cos({b}) = {Cos(b)}\n");

		return 0;
	}	
	
}
