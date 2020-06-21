using static System.Math;
using static System.Console;
using System;

// Make a subclass specefically for the gaussian activation function:
public class annODE : ann {
	// The explicit activation function:
	static Func<double, double> gaussian_f = (x) => Exp(-x*x);
	// The (indefinite) integral of the activation function:
	// Uses the stirlings approximation based gamma function, from the
	// week 4 exercise:
	// static Func<double, double> gaussian_int = (x) => math.erf(x);
	// The derivative of the activation function:
	// Uses the Abramowitz and Stegun gamma function from the week 4
	// exercise:
	static Func<double, double> gaussian_prime = (x) => -2*x*Exp(-x*x);

	static Func<double, double> gaussian_2prime = (x) => (4*x*x - 2)*Exp(-x*x);
	
	// Make the constructor of the subclass call the constructor of the
	// parent class:
	public annODE(int hiddenNodes, int maxMinimizationSteps=10000) : base(hiddenNodes, gaussian_f, maxMinimizationSteps:maxMinimizationSteps) { }

	public double feedforward_prime(double x) {
		return feedforward_prime(x, param);
	}
	// 
	public double feedforward_prime(double x, vector paramVec) {
		double res = 0;
		for(int i = 0; i < n; i++) {
			double a = paramVec[3*i+0];
			double b = paramVec[3*i+1];
			double w = paramVec[3*i+2];
			res += gaussian_prime((x - a)/b)*w/b;
			// res += gaussian_prime((x - paramVec[3*i+0])/paramVec[3*i+1])*paramVec[3*i    +2]/paramVec[3*i+1];
		}
		return res;
	}

	public double feedforward_2prime(double x) {
		return feedforward_2prime(x, param);
	}
	// d^2/dx^2 f((x-a)/b) = 1/b^2 * f''((x-a)/b):
	public double feedforward_2prime(double x, vector paramVec) {
		double res = 0;
		for(int i = 0; i < n; i++) {
			double a = paramVec[3*i+0];
			double b = paramVec[3*i+1];
			double w = paramVec[3*i+2];
			// res += gaussian_2prime((x - paramVec[3*i+0])/paramVec[3*i+1])*paramVec[3*i+2]/(paramVec[3*i+1]*paramVec[3*i+1]);
			res += gaussian_2prime((x - a)/b)*w/b/b;
		}
		return res;
	}
	
	public void train(
		double a,
		double b,
		double c, 
		double yc, 
		double yc_prime, 
		Func<double, double, double, double, double> Phi,
		int maxSteps = 10000
	) {
		double eps = 1e-7;
		// Define the deviation function:
		Func<vector, double> deviation = (paramVec) => {
			double delta = 0;
			// Calculate and add the integral part:
			Func<double, double> integrand = (x) => 
				Pow(Phi(feedforward_2prime(x, paramVec), feedforward_prime(x, paramVec), feedforward(x, paramVec), x), 2); 
			int evals = 0;
			delta += integrator.O4AT(integrand, a, b, eps, eps, ref evals);
			// Error.Write($"Integral part of delta = {delta}\n");	
			// Calculate the deviation of the value and add it:
			delta += Pow(Abs(feedforward(c, paramVec) - yc), 2)*(b-a);
			
			// Calculate the deviation of the derivative and add it:
			delta += Pow(Abs(feedforward_prime(c, paramVec) - yc_prime), 2)*(b-a);
			// Print the deviation for debugging:
			// Error.Write($"delta = {delta}\n");
			return delta;
		};
		
		// Define the starting vector:
		vector xstart = new vector(3*n); 
		vector xstartStep = new vector(3*n);
		// Dimitri suggested something like this: 
		double xstep = (b - a)/(n-1); 
		for(int i = 0; i < n; i++) { 
			xstart[3*i+0] = a + i*xstep; 
			xstart[3*i+1] = 1; 
			xstart[3*i+2] = 1; 

			double factor = 0.5;

			xstartStep[3*i+0] = factor*xstep;
			xstartStep[3*i+1] = factor;
			xstartStep[3*i+2] = factor;
		} 
		
		simplex minimizer = new simplex(deviation, 3*n);
		Error.Write("Initialized the minimizer!\n");		
		double stepsize = 1;	
	
		_minimizationSteps = minimizer.search(xstart, xstartStep, eps, maxSteps:maxSteps);

		Error.Write($"Minimization steps = {_minimizationSteps}\n");	
		param = minimizer.minimum;		

	}

}
