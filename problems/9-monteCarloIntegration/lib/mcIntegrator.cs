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
		double sigma = Sqrt(sum2/N - sum/N*sum/N);
		double SIGMA = sigma/Sqrt(N);
			
		// Return the integral value and the error in a vector:
		return new vector(mean*volume, SIGMA*volume);
	}
	
	public static double[] stratisfiedmc(
		Func<vector, double> f,
		vector a,
		vector b,
		int N
	) {
		// calculate the integration volume:
		double volume = 1.0;
		for(int i = 0; i < a.size; i++) {
			volume *= b[i] - a[i];
		}
		
		// calculate the mean value and variance of the function:
		double[] result = stratisfiedmc_step(f, a, b, N);
		double mean = result[0];
		double var = result[1];		
		
		// The integrated value is the mean of the function times the integration volume:
		double Q = volume*mean;
		
		// Error comes from the central limit theorem as: volume*sqrt(var)/sqrt(N):
		double error = volume*Sqrt(var)/Sqrt(N);

		// Return the integration value and the standard devaition:
		return new double[] {Q, error};
		
	}

	public static double[] stratisfiedmc_step(
		Func<vector, double> f, // The function to integrate
		vector a, // The starting points vector
		vector b, // The ending points vector
		int N, // The amount of points to sample		
		int minN=200 // The minimum sample size
	) {
		//Error.Write($"\nOne call to stratisfiedmc_step, N = {N}\n");
		//Error.Write($"a = {a},   b = {b}\n");
		// Define the point maker algorith, randomx:
		var rand = new Random();
		Func<vector> randomx = delegate() {
			vector x = new vector(a.size);
			for(int i = 0; i < x.size; i++) {
				double randDouble = rand.NextDouble();
				//Error.Write($"randDouble = {randDouble}, ");
				x[i] = a[i] + randDouble*(b[i] - a[i]);
			}
			return x;
		};
		// Sample N/20 points:
		int subN = N/20;
		
		// In the case of too few samples, reuse some code:
		if(N < minN) {
			// Error.Write("N < minN!\n");
			subN = N;
		}
		// Error.Write($"subN = {subN}\n");
		
		vector upperSum = new vector(a.size);
		vector lowerSum = new vector(a.size);
		vector upperSum2 = new vector(a.size);
		vector lowerSum2 = new vector(a.size);
		for(int i = 0; i < subN; i++) {
			vector x = randomx();
			double fx = f(x);
			//Error.Write($"x = {x}, fx = {fx}\n");
			// Calculate the sum of all the "upper" and "lower" points
			// for each dimmension. And the sum of squares:
			for(int j = 0; j < a.size; j++) {
				if(x[j] > (a[j]+b[j])/2) {
					upperSum[j]  += fx;
					upperSum2[j] += fx*fx;
				} else {
					lowerSum[j]  += fx;
					lowerSum2[j] += fx*fx;
				}
			}	
		}
		// Error.Write($"Upper Sum: {upperSum}\n");
		//Error.Write($"Lower Sum: {lowerSum}\n");
		
		// Calculate the mean and variance of this sample:
		double subMean = (upperSum[0] + lowerSum[0])/subN;
		double subVar = ((upperSum2[0]+lowerSum2[0])/subN - subMean*subMean);	
	
		// If N < minN, remember subN = N, and return the mean and var of
		// the entire sample:
		if(N < minN) {
			// Error.Write($"Returning simple sample, subMean = {subMean}, subVar = {subVar}\n");
			return new double[] {subMean, subVar};
		}

		// Now to find the best splitting dimmension:
		double highestVar = 0;
		int highestVarIdx = 0;
		double highestUpperVar = 0;
		double highestLowerVar = 0;
		for(int i = 0; i < a.size; i++) {
			double upperVar = upperSum2[i]/subN - (upperSum[i]/subN)*(upperSum[i]/subN);
			double lowerVar = lowerSum2[i]/subN - (lowerSum[i]/subN)*(lowerSum[i]/subN);
			// What is the highest variance:
			double higherVar = (upperVar > lowerVar)?upperVar:lowerVar;
			if(higherVar > highestVar) {
				highestVar = higherVar;
				highestVarIdx = i;
				highestUpperVar = upperVar;
				highestLowerVar = lowerVar;
			}
		}
		// Now the best splitting dimmension is the i'th dimmension where
		// i = highestVarIdx.
		// Error.Write($"Splitting along the {highestVarIdx}'th dimmension\n");
		// Error.Write($"highestUpperVar = {highestUpperVar}, highestLowerVar = {highestLowerVar}\n");
		
		// How many points are left?
		int leftN = N - subN;
		// Distribute the points left:
		int upperN = (int)(leftN*highestUpperVar/(highestUpperVar+highestLowerVar));
		
		int lowerN = leftN-upperN;
		// Error.Write($"leftN: {leftN},    upperN: {upperN},  lowerN: {lowerN}\n");
		
		// Calculate the new a and b vectors:
		vector uppera = a.copy();
		uppera[highestVarIdx] = (a[highestVarIdx] + b[highestVarIdx])/2;
		vector lowerb = b.copy();
		lowerb[highestVarIdx] = (a[highestVarIdx] + b[highestVarIdx])/2;
		 
		// Do the recursive call to get the mean and variance of the 
		// two split regions:
		double[] upperResult = stratisfiedmc_step(f, uppera, b, upperN);
		double upperMeanRes = upperResult[0];
		double upperVarRes = upperResult[1];

		double[] lowerResult = stratisfiedmc_step(f, a, lowerb, lowerN);
		double lowerMeanRes = lowerResult[0];
		double lowerVarRes = lowerResult[1];
		
		// Error.Write($"subMean = {subMean}, subN = {subN}, upperMeanRes = {upperMeanRes}, upperN = {upperN},  lowerMeanRes = {lowerMeanRes}, lowerN = {lowerN}\n");
		// calculate the total mean by a weighted sum:
		// double totalMean = (subMean*subN + upperMeanRes*upperN + lowerMeanRes*lowerN)/N;
		// Maybe just mean of means? Makes good sense...
		double totalMean = (subMean + upperMeanRes + lowerMeanRes)/3;
		// Calculate the total variance by a weighted sum?
		// E(x) = sum(w_i*u_i) => var(x) = sum(w_i^2*var_i)
		// double totalVar = ((subVar*subN*subN) + (upperVarRes*upperN*upperN) + (lowerVarRes*lowerN*lowerN))/(N*N);
		// Not weighted sum though:
		double totalVar = (subVar + upperVarRes + lowerVarRes)/(3*3);

		// Error.Write("\n------------------------------\n");
		// Error.Write($"N = {N}, a = {a}, b = {b}\n"); 
		// Error.Write($"totalMean = {totalMean}, totalVar = {totalVar}\n");
		// Okay? totalSigma = sqrt(totalVar)
		// deviation = totalSigma*volume?

		// return the values:
		return new double[] {totalMean, totalVar};

	}
}
