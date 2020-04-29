using static System.Math;
using static System.Console;
using System;

public class mcIntegrator {

	public static vector plainmc(
		Func<vector, double> f, // The function to integrate
		vector a, // The starting points vector
		vector b, // The ending points vector
		int N // The amount of points to sample		
	) {
		// Make a rondomness generator:
		var rand = new Random();
		// 1: Make a funciton that gives random points in the integration
		// area:
		// This Func has no parameters, and returns a vector
		Func<vector> randomx = delegate() {
			vector x = new vector(a.size);
			for(int i = 0; i < x.size; i++) {
				x[i] = a[i] + rand.NextDouble()*(b[i] - a[i]);
			}
			return x;
		};
		
		// 2: Calculate the integration volume:
		double volume = 1.0;
		for(int i = 0; i < a.size; i++) {
			volume *= b[i] - a[i];
		}
		//Error.Write($"Integration volume: {volume}\n");
		
		// 3: Sample the function in N random points.
		// Calculating the sum of values: 
		double sum = 0;
		// and sum of squares of values
		double sum2 = 0;
		for(int i = 0; i < N; i++) {
			vector thisx = randomx();
			double fx = f(thisx);
			//Error.Write($"thisx = {thisx}\n");
			//Error.Write($"f(thisx) = {fx}\n");
			sum += fx;
			sum2 += fx*fx;
		}
		//Error.Write($"sum: {sum}\n");
		//Error.Write($"sum2: {sum2}\n");
		
		// Calculate the mean value:
		double mean = sum/N;		
		//Error.Write($"mean: {mean}\n");
		//Error.Write($"mean*volume: {mean*volume}\n");		
		// Calculate the deviation, small sigma:
		double sigma = Sqrt(sum2/N - mean*mean);
		double SIGMA = sigma/Sqrt(N);
		
		// Return the integral value and the error in a vector:
		return new vector(mean*volume, SIGMA*volume);
	}

}
