using System;
using static System.Console;
using static System.Math;
using System.Collections;
using System.Collections.Generic;
class main {
	static int Main() {
		// Define the vector ODE function:
		// y0 = u
		// y1 = u'
		//  u'(x) = u(x) * (1-u(x))
		// y0' = u' = u(x) * (1-u(x)) = y0 * (1-y0)
		// y1' = ? = 0
		// y[1] is then totally unsued and doesn't matter at all:
		Func<double, vector, vector> logistic = delegate(double x, vector y) {
			return new vector(y[0]*(1-y[0]), 0);
		};
		double xa = 0;
		double xb = 3;
		
		vector ya = new vector(0.5, 0);
		
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();
	
		vector yb = ode.rk23(logistic, xa, ya, xb, xlist:xs, ylist:ys);
		
		double trueValue;
		for(int i = 0; i < xs.Count; i++) {
			// I don't think i need the first derivative of the function in
			// my data file?
			trueValue = 1/(1+Exp(-xs[i]));
			Write("{0:f15} {1:f15} {2:f15}\n", xs[i], ys[i][0], trueValue);
		}		
		
		return 0;

	}
}
