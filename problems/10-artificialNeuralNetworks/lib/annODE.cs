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
	static Func<double, double> gaussian_int = (x) => math.erf(x);
	// The derivative of the activation function:
	// Uses the Abramowitz and Stegun gamma function from the week 4
	// exercise:
	static Func<double, double> gaussian_prime = (x) => -2*x*Exp(-x*x);

	static Func<double, double> gaussian_2prime = (x) => (4*x*x - 2)*Exp(-x*x);
	
	// Make the constructor of the subclass call the constructor of the
	// parent class:
	public annODE(int hiddenNodes) : base(hiddenNodes, gaussian_f) { }

	// 
	public double feedforward_prime(double xi, vector paramVec) {
		double res = 0;
		for(int i = 0; i < n; i++) {
			res += gaussian_prime((x - paramVec[3*i+0])/paramVec[3*i+1])*paramVec[3*i    +2]/paramVec[3*i+1];
		}
		return res;
	}

	// 
	public double feedforward_2prime(double x) {
		double res = 0;
		for(int i = 0; i < n; i++) {
			res += gaussian_2prime((x - param[3*i+0])/param[3*i+1])*param[3*i+2]/(param[3*i+1]*param[3*i+1]);
		}
		return res;
	}
	
	
	public override void train(
		double c, 
		double yc, 
		double yc_prime, 
		Func<double, double, double, double, double> Phi
	) {
		Func<vector, double>
	}

}
