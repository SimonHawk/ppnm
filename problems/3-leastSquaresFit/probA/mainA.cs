using System;
using static System.Console;
using static System.Math;

class mainA {
	static void Main() {
		Func<double, double> errorFunc = (x) => x / 20;
		data rawDat = new data("data.A.txt", errorFunc);
		Write("Test reading files:\n");
		rawDat.xs.print("xs = ");
		rawDat.ys.print("ys = ");
		rawDat.dys.print("dys = ");
			
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
		
		Write($"ln(a) = {cs[0]}, lambda = {-cs[1]}\n");
		
		var writer = new System.IO.StreamWriter("out.bestFit.txt");
		
		Func<double, double> fitFun = x => {
			double sum = 0;
			for(int i = 0; i < fs.Length; i++ ) {
				sum += cs[i]*fs[i](x);
			}
			return sum;
		};

		for(double x = 0+0.001; x < 20; x += 0.1) {
			writer.Write("{0:f16} {1:f16}\n", x, Exp(fitFun(x)));
		}
		writer.Close();
	}
}
