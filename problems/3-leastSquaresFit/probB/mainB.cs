using System;
using static System.Console;
using static System.Math;

class mainB {
	static void Main() {
		Func<double, double> errorFunc = (x) => x / 20;
		data rawDat = new data("../data.A.txt", errorFunc);
			
		// Recalculate the data on the logarithmic form.
		vector logYs = new vector(rawDat.xs.size);
		vector dlogYs = new vector(rawDat.xs.size);	
		for(int i = 0; i < rawDat.xs.size; i++) {
			logYs[i] = Log(rawDat.ys[i]);
			dlogYs[i] = rawDat.dys[i]/rawDat.ys[i];
		}
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
		Sigma.print("Covariance Matrix: ");
		vector dc = calculate_dc(Sigma);
		
		Write($"ln(a) = {cs[0]} +/- {dc[0]}, lambda = {-cs[1]} +/- {dc[1]}\n");
		Write($"a = {Exp(cs[0])}, t_1/2 = {Log(2)/(-cs[1])}");
		var writer = new System.IO.StreamWriter("out.bestFitB.txt");
		
		Func<double, double> F = makeFitFun(fs, cs);
		Func<double, double> F_pdc = makeFitFun(fs, cs+dc);
		Func<double, double> F_mdc = makeFitFun(fs, cs-dc);

		for(double x = 0; x < 20; x += 0.1) {
			writer.Write("{0:f16} {1:f16} {2:f16} {3:f16}\n", x, Exp(F(x)), Exp(F_pdc(x)), Exp(F_mdc(x)) );
		}
		writer.Close();
	}

	static vector calculate_dc(matrix Sigma) {
		vector dc = new vector(Sigma.size1);
		for(int i = 0; i < dc.size; i++) {
			dc[i] = Sqrt(Sigma[i, i]);
		}
		return dc;
	}
	
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
