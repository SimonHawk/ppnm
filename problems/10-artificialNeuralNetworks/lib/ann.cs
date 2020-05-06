using static System.Math;
using static System.Console;
using System;

// Container class for the artificial neural network.
// This will consist of one entry neuron, a layer of n hidden 
// neurons and a summation output neuron:
// Its purpose is to interpolate a tabulated function:
public class ann {
	int n; // The number of hidden nodes
	Func<double,double> f; // The activation function
	vector param; // The parameters for the network, a_i, b_i and w_i
				  // for each hidden layer neuron
			      // Ordered so that a_i = param[3*i+0], b_i = param[3*i+1]
				  // and w_i = param[3*i+2]

	// constructor for the class:
	public ann(int hiddenNodes, Func<double, double> activationFunc) {
		n = hiddenNodes;
		f = activationFunc;
		// param will be all zeros for now
		param = new vector(3*n);

	}


	// Method to apply the network to the input x.
	public double feedforward(double x) {
		return feedforward(x, param);
	}
	
	double feedforward(double x, vector paramVec) {
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
		for(int i = 0; i < n; i++) {
			xstart[i+0] = i;
			xstart[i+1] = i;
			xstart[i+2] = i;
		}
		
		// The accuracy goal of the minimization:
		double eps = 1e-8;		

		vector xopt = minimization.qnewton(deviation, xstart, eps);
		
		param = xopt;		

	}

} 
