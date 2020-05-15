using static System.Console;
using static System.Math;
using System;

class mainC {

	static void Main() {
		
		// Define the differential equation:
		// d^2/dx^2 sin(x) = -sin(x)
		// => f2p + f = 0:
		Func<double, double, double, double, double> diff_eq = (f2p, fp, f, x) => f2p + f;
		
		// Define the limits:
		double a = 0.0;
		double b = 2*PI;
		
		// Define the known position:
		double c = PI;
		// Value of function in c:
		double yc = 0.0;
		// Value of derivative in c:
		double ypc = -1.0;
		
		// Make the neutral network, 10 nodes:
		int n = 10;
		annODE network = new annODE(n);
		
		// Train the network on the function:
		network.train(a, b, c, yc, ypc, diff_eq);
	
		// Export data to make figure:
		var outfile = new System.IO.StreamWriter("out.dataC.txt");
		int NPoints = 100;
		for(double x = a; x <= b; x += (b-a)/NPoints) {
			outfile.Write("{0} {1} {2}\n", x, network.feedforward(x), network.feedforward_prime(x));
		}
		outfile.Close();

		
	}
	
}
