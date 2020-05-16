using System;
using static System.Console;
using static System.Math;

class mainB {
	static void Main() {
		Write("Problem B:\n");
		// Define the function that transforms the y parameters to the
		// uncertainties in the y parameters. In this case, dy = y/20:
		Func<double, double> errorFunc = (x) => x / 20;
		// Load the data points from the file data.A.txt (In the super
		// folder), constructing the errors of the data from the above 
		// defined function:
		// This is saved in an object of the simple "data" class.
		data rawDat = new data("../data.A.txt", errorFunc);
			
		// Recalculate the data on the logarithmic form.
		vector logYs = new vector(rawDat.xs.size);
		vector dlogYs = new vector(rawDat.xs.size);	
		for(int i = 0; i < rawDat.xs.size; i++) {
			logYs[i] = Log(rawDat.ys[i]);
			dlogYs[i] = rawDat.dys[i]/rawDat.ys[i];
		}
		// Save the data in a new data object, calling a different
		// constructor:
		data logDat = new data(rawDat.xs, logYs, dlogYs);
		
		// Make the series of functions to use for least squares:
		// A constant and a linear function.
		Func<double, double>[] fs = new Func<double, double>[]{x => 1, x => x};
		
		// Now make the fit. For the model ln(y) = ln(a) - lambda*t, this
		// will give:
		// cs[0] = ln(a)
		// cs[1] = -lambda
		vector cs = leastSquares.calculateC(logDat, fs);
		matrix Sigma = leastSquares.calculateSigma(logDat, fs);
		Write("Fitting the same data to the same function as in problem A, but now finding uncertainties:\n");
		Sigma.print("Found covariance matrix:");
		vector dc = calculate_dc(Sigma);
		
		// Write out the result:
		Write("\nThe found fitting parameters with uncertainties:\n");
		Write($"ln(a)    = {cs[0]:f6} +/- {dc[0]:f6}\n");
		Write($"lambda   = {-cs[1]:f6} +/- {dc[1]:f6}\n");
		
		// Calculate uncertainties of a and t_1/2. Note: Simple error 
		// propagation is sufficient, as the covariances do not contribute
		// to the uncertainties in this case.
		double da = Exp(cs[0])*dc[0];
		double dt12 = Log(2)/((-cs[1])*(-cs[1]))*dc[1];
		Write("\nCalculating the uncertainties of a and t_1/2 from the fitting parameters:\n");
		Write($"a = exp(ln(a)) => sigma_a = Sqrt(exp(ln(a)))^2*dln(a)^2)\n");
	//	Write($"sigma_a = {da:f6}\n");

		Write($"t_(1/2) = ln(2)/lambda => sigma_t_1/2 = ln(2)/lambda^2  * dlambda\n");
	//	Write($"sigma_t_1/2 = {dt12:f6}\n");
		
		Write($"\na       = {Exp(cs[0]):f6} +/- {da:f6}\n");
		Write($"t_1/2   = {Log(2)/(-cs[1]):f6} +/- {dt12:f6}\n");
		
		
		// Write out the functional valus in a new file, out.bestFitB.txt
		var writer = new System.IO.StreamWriter("out.bestFitB.txt");
		// Construct the functions to plot using a helper method:
		Func<double, double> F = makeFitFun(fs, cs);
		Func<double, double> F_pdc = makeFitFun(fs, cs+dc);
		Func<double, double> F_mdc = makeFitFun(fs, cs-dc);
		// Write out the data:
		for(double x = 0; x < 20; x += 0.1) {
			writer.Write("{0:f16} {1:f16} {2:f16} {3:f16}\n", x, Exp(F(x)), Exp(F_pdc(x)), Exp(F_mdc(x)) );
		}
		writer.Close();
	}

	// Function that calculates the uncertainties in the c parameters from
	// the covariance matrix Sigma:
	static vector calculate_dc(matrix Sigma) {
		vector dc = new vector(Sigma.size1);
		for(int i = 0; i < dc.size; i++) {
			dc[i] = Sqrt(Sigma[i, i]);
		}
		return dc;
	}
	
	// Function that returns the fitted function, given the basis of	
	// basis of function that were fitted to, fs, and the found fit
	// fitting coefficients cs:
	static Func<double, double> makeFitFun(Func<double, double>[] fs, vector cs) {
		return (x) => {
			double sum = 0;
			for(int i = 0; i < fs.Length; i++ ) {
				sum += cs[i]*fs[i](x);
			}
			return sum;
		};
	}
}
