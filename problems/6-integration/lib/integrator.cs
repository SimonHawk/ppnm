using System;
using static System.Console;
using static System.Math;

public class integrator {
	
	// Starting function for the open 4 point adaptive trapeziodal 
	// quadrature.
	public static double O4AT(Func<double, double> f, double a, double b, double delta, double eps, ref int evals) {
		return sub_O4AT(f, a, b, delta, eps, ref evals, null);
	}
	
	// The sub integrator will reuse points if applicaple:
	public static double sub_O4AT(Func<double, double> f, double a, double b, double delta, double eps, ref int evals, vector old_fs) {
		// The rescaled points to evaluate in:
		vector xi = new vector(new double[] {1.0/6, 2.0/6, 4.0/6, 5.0/6});
		// The actual points to evalue in: (I have implemented double+vector addition in the
		// the vector class)
		vector xri = a + (b-a)*xi;
		// The weights of different order:
		vector wi = new vector(new double[] {2.0/6, 1.0/6, 1.0/6, 2.0/6});
		vector vi = new vector(new double[] {1.0/4, 1.0/4, 1.0/4, 1.0/4});
		// The rescaled weights:
		vector wri = (b-a)*wi;
		vector vri = (b-a)*vi;
		
		// The points to evalue the function in: 
		// If old_fs is given, reuse old points:
		vector fs;
		if(old_fs == null) {
			fs = new vector(new double[] {f(xri[0]), f(xri[1]), f(xri[2]), f(xri[3])});
			evals += 4;
		} else {
			fs = new vector(new double[] {f(xri[0]), old_fs[0], old_fs[1], f(xri[3])});
			evals += 2;
		}
		// fs.print($"At {evals} evals, a = {a}, b = {b}, fs = ");

		// Calculate the two orders of quadratures:
		double Q = wri[0]*fs[0] + wri[1]*fs[1] + wri[2]*fs[2] + wri[3]*fs[3];
		double q = vri[0]*fs[0] + vri[1]*fs[1] + vri[2]*fs[2] + vri[3]*fs[3];
		// Write($"Q-q = {Q-q}\n");		
		double QAlt = (2*fs[0] + fs[1] + fs[2] + 2*fs[3])/6 * (b-a);
		double qAlt = (fs[0] + fs[1] + fs[2] + fs[3])/4 * (b-a);
		
		Q = QAlt;
		q = qAlt;
	
		// If error estimate is acceptable, return Q:
		if((Abs(Q-q)/2) < delta + eps*Abs(Q)) {
			//Error.Write($"Abs(Q-q): {Abs(Q-q)} < delta + eps*Abs(Q): {delta + eps*Abs(Q)}\n");
			return Q;
		}
		// Else, split interval in two, reuse availible points, and calculate
		// the intergral as the sum of the two intergrals:
		else {
			vector fs_left = new vector(new double[] {fs[0], fs[1]});
			vector fs_right = new vector(new double[] {fs[2], fs[3]});
			double int_left = sub_O4AT(f, a, (a+b)/2, delta/Sqrt(2), eps, ref evals, fs_left);
			double int_right = sub_O4AT(f, (a+b)/2, b, delta/Sqrt(2), eps, ref evals, fs_right);
			return int_left + int_right;
		}
	}
	
	public static double CC_O4AT(Func<double, double> f, double a, double b, double delta, double eps, ref int evals) {
		// First rescale the problem to be from -1 to 1:
		// u(-1) = -a, u(1) = b, u(x) = alpha*x+b =>
		Func<double, double> u = (x) => (b-a)/2*x + (b+a)/2;
		// Rescale f:
		Func<double, double> fu = (x) => f(u(x)) * (b-a)/2;
		
		// Now make the Clenshaw-Curtis transformation:
		Func<double, double> fcc = (theta) => fu(Cos(theta))*Sin(theta); 
	
		// Now integrate this with the regular old quadrature
		// Here using the open 4 point apaptive trapeziodal quadrature:
		return O4AT(fcc, 0, PI, delta, eps, ref evals);
	}


}
