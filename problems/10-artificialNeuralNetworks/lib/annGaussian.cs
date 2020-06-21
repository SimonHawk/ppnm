using static System.Math;
using static System.Console;
using System;

// Make a subclass specefically for the gaussian activation function:
public class annGaussian : ann {
	// The explicit activation function:
	static Func<double, double> gaussian_f = (x) => Exp(-x*x);
	// The (indefinite) integral of the activation function:
	// Uses the stirlings approximation based gamma function, from the
	// week 4 exercise:
	static Func<double, double> gaussian_int = (x) => Sqrt(PI)/2*math.erf(x);
	// The derivative of the activation function:
	// Uses the Abramowitz and Stegun gamma function from the week 4
	// exercise:
	static Func<double, double> gaussian_prime = (x) => -2*x*Exp(-x*x);
	
	// Make the constructor of the subclass call the constructor of the
	// parent class:
	public annGaussian(int hiddenNodes, int maxMinimizationSteps=1000) : base(hiddenNodes, gaussian_f, maxMinimizationSteps:maxMinimizationSteps) { }

	public double feedforward_prime(double x) {
		double res = 0;
		for(int i = 0; i < n; i++) {
			res += gaussian_prime((x - param[3*i+0])/param[3*i+1])*param[3*i+2]/param[3*i+1];
		}
		return res;
	}
		
	// Integral, based on int(f((x-a)/b)) = F((x-a)/b)*b
	public double feedforward_int(double x) {
		double res = 0;
		for(int i = 0; i < n; i++) {
			// Add integral from 0 to x:
			res += gaussian_int((x-param[3*i+0])/param[3*i+1])*param[3*i+2]*param[3*i+1];
			// Subtract integral from to leftmost point:
			res -=  gaussian_int((leftx-param[3*i+0])/param[3*i+1])*param[3*i+2]*param[3*i+1];
		}
		return res;
	}

}
