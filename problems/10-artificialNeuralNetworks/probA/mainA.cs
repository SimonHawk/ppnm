using static System.Console;
using static System.Math;
using System;

class mainA {
	
	static void Main() {
		Write("Problem A\n");
		// Define the activation function as a gaussian:
		Func<double, double> gaussian = (x) => Exp(-x*x);
		// Make a 10 node network:
		int n = 10;
		ann gaussian10 = new ann(n, gaussian);
		
		// Quick and dity test on sine funtion:
		double xmin = 0;
		double xmax = 2*PI;
		int NPoints = 20;
		
		vector xs = new vector(NPoints);
		vector ys = new vector(NPoints);
				
		for(int i = 0; i < NPoints; i++) {
			double x = (xmax-xmin)/(NPoints-1) * i;
			xs[i] = x;
			ys[i] = Sin(x);	
		}
	
		gaussian10.train(xs, ys);
		Write("Completed training the sin function:\n");
		Write($"Sampled sine points:         {NPoints}\n");
		Write($"Minimization accuracy goal:  {gaussian10.minimizationEps}\n");
		Write($"Minimization steps:          {gaussian10.minimizationSteps}\n");
		Write($"(OBS: Accuracy goal was not reached as maximum number of steps was reached)\n");
		
		var outfile = new System.IO.StreamWriter("out.dataA.txt");
		int plotPoints = 500;
		for(double x = xmin; x <= xmax; x += (xmax - xmin)/plotPoints) {
			outfile.Write("{0} {1} {2}\n", x, gaussian10.feedforward(x), Sin(x));
		}
		// Split the file in two sections:
		outfile.Write("\n\n");
		// Write down the sampled sine points:
		for(int i = 0; i < NPoints; i++) {
			outfile.Write("{0} {1}\n", xs[i], ys[i]);
		}
		outfile.Close();
		
		
		


	}

}

