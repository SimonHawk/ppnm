using static System.Math;
using static System.Console;
using System;

class mainB {
	static void Main() {
		// Load the data:
		data higgsData = new data("higgsData.txt");
		
		// Define the fitting function:
		Func<double, vector, double> fitFunc = (x, fitParams) => {
			double m = fitParams[0];
			double Gamma = fitParams[1];
			double A = fitParams[2];
			return A/((x - m)*(x - m) + Gamma*Gamma/4);		
		};

		// Starting parameters:
		vector xstart = new vector(126.0, 5, 15);
		
		// The accuracy goal of the fitting function:
		double eps = 1e-6;
		int steps = 0;

		// Fit to the data:
		vector fitResult = dataFitter.fitToFunction(higgsData, fitFunc, xstart, ref steps, eps);
		
		Write("Fitting the Briet-Wigner function to the Higgs boson data:\n");
		Write($"Accuracy goal of minimazation: {eps}\n");
		Write($"Guessed starting parameters:\n");
		Write($"Guessed m:       {xstart[0]}\n");
		Write($"Guessed Gammma:  {xstart[1]}\n");
		Write($"Guessed A:       {xstart[2]}\n");
		Write($"Found parameters:\n");
		Write($"m:               {fitResult[0]}\n");
		Write($"Gammma:          {fitResult[1]}\n");
		Write($"A:               {fitResult[2]}\n");
		Write($"Steps used:      {steps}\n");
		
		// Export fit curve:
		var outfile = new System.IO.StreamWriter("out.probB.txt");
		for(double x = 101.0; x <= 159.0; x += 0.3) {
			outfile.Write("{0} {1}\n", x, fitFunc(x, fitResult));
		}
		outfile.Close();

	}
}
