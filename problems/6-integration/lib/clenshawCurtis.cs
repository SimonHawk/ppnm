using System;
using static System.Math;
using static System.Console;


// Class to hold the Clenshaw-Curtis variable transformation integrator.
// I am told to make it adaptive. Hereby, it makes sense to chose points
// than can be reused?
// I am told to make it open quadrature. How should i choose the nodes?
class integrator_CC {

	public sub_integrate_CC(Func<double, double> f, double a, double b, double delta, double eps, ref int evals) {
		// First rescale the problem to be from -1 to 1:
		// u(a) = -1, u(b) = 1, u(x) = alpha*x+b =>
		Func<double, double> u = (x) => 2/(b-a)*x - (b-a)/(2*a);
		// Rescale f:
		Func<double, double> fu = (x) => f(u(x));
		
		// Now make the Clenshaw-Curtis transformation:
		Func<double, double> fcc = (theta) => fu(Cos(theta))*Sin(theta);

		// Now integrate this with the regular old quadrature
		// using the right limits? 
		// Here using the open 4 point apaptive trapeziodal quadrature:
		return O4AT(fcc, 0, PI, delta, eps, ref evals);
	}
	
	
		

}


