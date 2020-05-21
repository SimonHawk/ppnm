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
		int n = 20;
		annODE network = new annODE(n);
		
		// Train the network on the function:
		Error.Write("OBS! Starting training for u'' = -u. This takes around 20 seconds on my computer!\n");
		network.train(a, b, c, yc, ypc, diff_eq);
	
		// Export data to make figure:
		var outfile = new System.IO.StreamWriter("out.dataC.txt");
		int NPoints = 100;
		for(double x = a; x <= b; x += (b-a)/NPoints) {
			outfile.Write("{0} {1} {2} {3}\n", x, network.feedforward(x), network.feedforward_prime(x), network.feedforward_2prime(x));
		}

		// ------------------------------------------
		// Now for a "harder" differential equation:	
		// The bessel equation:
		double alpha = 0;
		Func<double, double, double, double, double> diff_eq2 = 
			(f2p, fp, f, x) => x*x*f2p + x*fp + (x*x - alpha*alpha)*f;
		
		// Define the limits:
		double a2 = 0.0;
		double b2 = 4.0;
		
		// Define the known position:
		double c2 = 0;
		// Value of function in c:
		double yc2 = 1.0;
		// Value of derivative in c:
		double ypc2 = 0;
		
		// Make the neutral network, 10 nodes:
		int n2 = 10;
		annODE network2 = new annODE(n);
	
		Error.Write("OBS! Starting training for bessel equations, this takes around 100 seconds on my computer!\n");
		network2.train(a2, b2, c2, yc2, ypc2, diff_eq2);
		
		// Write it out:
		outfile.Write("\n\n");
				
		for(double x = a2; x <= b2; x += (b2-a2)/NPoints) {
			outfile.Write("{0} {1} {2} {3}\n", x, network2.feedforward(x), network2.feedforward_prime(x), network2.feedforward_2prime(x));
		}
		
		outfile.Close();		

	}
	
}
