using static System.Math;
using static System.Console;
using System;

// Container class for the artificial neural network.
// This will consist of one entry neuron, a layer of n hidden 
// neurons and a summation output neuron:
// Its purpose is to interpolate a tabulated function:
public class ann {
	protected int n; // The number of hidden nodes
	Func<double,double> f; // The activation function
	protected vector param; // The parameters for the network, a_i, b_i and w_i
				  // for each hidden layer neuron
			      // Ordered so that a_i = param[3*i+0], b_i = param[3*i+1]
				  // and w_i = param[3*i+2]
	protected int _minimizationSteps; // The steps in the minimization.
	public int minimizationSteps {get{return _minimizationSteps;}}
	
	protected double _minimizationEps;
	public double minimizationEps {get{return _minimizationEps;} set{_minimizationEps = value;}}

	protected double leftx;
	
	int _maxMinimizationSteps;

	// constructor for the class:
	public ann(int hiddenNodes, Func<double, double> activationFunc, int maxMinimizationSteps=1000) {
		n = hiddenNodes;
		f = activationFunc;
		// param will be all zeros for now
		param = new vector(3*n);
		_minimizationSteps = 0;
		_minimizationEps = 1e-6;
		leftx = 0;
		_maxMinimizationSteps = maxMinimizationSteps;

	}


	// Method to apply the network to the input x.
	public double feedforward(double x) {
		return feedforward(x, param);
	}
	
	protected double feedforward(double x, vector paramVec) {
		// Initiate the sum:
		double y = 0;
		// Calculate the sum:
		for(int i = 0; i < n; i++)  {
			y += f((x-paramVec[3*i+0])/paramVec[3*i+1])*paramVec[3*i+2];
		}
		// return the sum:
		return y;
	}
	
	// Method to train the network to find the best interpolation of the table:
	public void train(vector xs, vector ys) {
		//Error.Write("Starting training!\n");
		Func<vector, double> deviation = (paramVec) => {
			double sum = 0;
			for(int k = 0; k < xs.size; k++) {
				double dist = feedforward(xs[k], paramVec) - ys[k];
				sum += dist*dist;
			}
			return sum;
		};
		
		// Initiate the starting vector... Kind of hard to guess...
		vector xstart = new vector(3*n);
		// Dimitri suggested something like this:
		double xmin = xs[0];
		double xmax = xs[xs.size-1];
		double xstep = (xmax - xmin)/(n-1);
		for(int i = 0; i < n; i++) {
			xstart[3*i+0] = xmin + i*xstep;
			xstart[3*i+1] = xstep;
			xstart[3*i+2] = 1;
		}
		
		// Error.Write($"xstart = {xstart}\n");

		//Error.Write($"Starting minimization! (maxSteps = {_maxMinimizationSteps})\n");
		vector xopt = minimization.qnewton(deviation, xstart, _minimizationEps, ref _minimizationSteps, maxSteps:_maxMinimizationSteps);
		
		leftx = xs[0];
		param = xopt;		
		// param = xstart;
		//Error.Write("Training finised!\n");

	}

} 
