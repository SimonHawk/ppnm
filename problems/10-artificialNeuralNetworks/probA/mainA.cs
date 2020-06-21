using static System.Console;
using static System.Math;
using System;

class mainA {
	
	static void Main() {
		Write("Problem A\n");
		// Define the activation function as a gaussian:
		Func<double, double> gaussian = (x) => Exp(-x*x);
		// Make a 10 node network:
		int maxSteps = 1000;
		ann gaussian5 = new ann(5, gaussian, maxMinimizationSteps:maxSteps);
		ann gaussian11 = new ann(11, gaussian, maxMinimizationSteps:maxSteps);
		ann gaussian17 = new ann(17, gaussian, maxMinimizationSteps:maxSteps);
		
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
	
		gaussian5.train(xs, ys);
		gaussian11.train(xs, ys);
		gaussian17.train(xs, ys);
		Write("Completed training the sin function for 5, 11 and 17 nodes:\n");
		Write($"Sampled sine points:         {NPoints}\n");
		Write($"Minimization accuracy goal:  {gaussian11.minimizationEps}\n");
		Write($"Minimization steps:          {gaussian11.minimizationSteps}\n");
		Write($"(OBS: Accuracy goal was not reached as maximum number of steps was reached)\n");

		Write($"\nLooking at figure A.interpolation.svg it seems that while the 11 node interpolation hits the points better than the 5 node one, it is more susceptible to wiggle. However, the 17 node interpolation does not exhibit this wiggle.\n");
		
		var outfile = new System.IO.StreamWriter("out.dataA.txt");
		int plotPoints = 500;
		for(double x = xmin; x <= xmax; x += (xmax - xmin)/plotPoints) {
			outfile.Write("{0} {1} {2} {3} {4}\n", x, Sin(x), gaussian5.feedforward(x), gaussian11.feedforward(x), gaussian17.feedforward(x));
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

