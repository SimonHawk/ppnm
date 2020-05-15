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
	public annODE(int hiddenNodes) : base(hiddenNodes, gaussian_f) { }

	public double feedforward_prime(double x) {
		return feedforward_prime(x, param);
	}
	// 
	public double feedforward_prime(double x, vector paramVec) {
		double res = 0;
		for(int i = 0; i < n; i++) {
			res += gaussian_prime((x - paramVec[3*i+0])/paramVec[3*i+1])*paramVec[3*i    +2]/paramVec[3*i+1];
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
			res += gaussian_2prime((x - paramVec[3*i+0])/paramVec[3*i+1])*paramVec[3*i+2]/(paramVec[3*i+1]*paramVec[3*i+1]);
		}
		return res;
	}
	
	public void train(
		double a,
		double b,
		double c, 
		double yc, 
		double yc_prime, 
		Func<double, double, double, double, double> Phi
	) {
		double eps = 1e-6;
		// Define the deviation function:
		Func<vector, double> deviation = (paramVec) => {
			double delta = 0;
			// Calculate and add the integral part:
			Func<double, double> integrand = (x) => 
				Abs(Phi(feedforward_2prime(x, paramVec), feedforward_prime(x, paramVec), feedforward(x, paramVec), x)); 
			int evals = 0;
			delta += integrator.O4AT(integrand, a, b, Sqrt(eps), eps, ref evals);
			
			// Calculate the deviation of the value and add it:
			delta += Abs(feedforward(c, paramVec) - yc)*(b-a);
			
			// Calculate the deviation of the derivative and add it:
			delta += Abs(feedforward_prime(c, paramVec) - yc_prime)*(b-a);
			return delta;
		};
		
		// Define the starting vector:
		vector xstart = new vector(3*n); 
		// Dimitri suggested something like this: 
		double xstep = (b - a)/(n-1); 
		for(int i = 0; i < n; i++) { 
			xstart[3*i+0] = a + i*xstep; 
			xstart[3*i+1] = xstep; 
			xstart[3*i+2] = 1; 
		} 

		vector xopt = minimization.qnewton(deviation, xstart, eps, ref _minimizationSteps);
		
		param = xopt;		

	}

}
