using System;
using static cmath;

public static partial class funcs {
	public static double S(double x) {
		Func<double, double> S_fun = t => sin(t*t);
		return quad.o8a(S_fun, 0.0, x);
	}
	
	public static double C(double x) {
		Func<double, double> C_fun = t => cos(t*t);
		return quad.o8a(C_fun, 0.0, x);
	}
	
}
