using static System.Math;
using static System.Console;
using System;

public static class dataFitter {

	public static vector fitToFunction(
		data dat, // The data to fit to
		Func<double, vector, double> fitFunction, // The fitting function
		vector xstart, // The starting parameters
		ref int steps,
		double eps=1e-6 // The accuracy goal of the minimization
	) {
		
		//Make the objective function:	
		Func<vector, double> objFunc = makeObjectiveFunction(fitFunction, dat);
		
		// Do the optimization:
		vector fitResult = minimization.qnewton(objFunc, xstart, eps, ref steps);
		
		// Return the fitresult:
		return fitResult;
	}
	

	// maker for the objectivefunction: 
	public static Func<vector, double> makeObjectiveFunction(
		Func<double, vector, double>  fitFunction, // (x, params) => y
		data dat	
	) {
		return (fitParams) => {
			double sum = 0;
			for(int i = 0; i < dat.xs.size; i++) {
				sum += Pow((fitFunction(dat.xs[i], fitParams)-dat.ys[i])/dat.sigma_ys[i], 2);
			}
			return sum;
		};
	}
			
}


// Helper class to read and store data 
public class data {
	public vector xs, ys, sigma_ys;
	
	public data(vector x, vector y, vector dy) {
		xs = x;
		ys = y;
		sigma_ys = dy;
	}

	// This constructor reads a two column file, 1st column is xs, second is        // ys, and calculates the dys as errorFunc(ys);
	public data(string filename) {
		var reader = new System.IO.StreamReader(filename);
		int fileLength = 0;
		while(reader.ReadLine()!=null) {
			fileLength += 1;
		}
		reader.Close();
		xs = new vector(fileLength);
		ys = new vector(fileLength);
		sigma_ys = new vector(fileLength);
		reader = new System.IO.StreamReader(filename);
		for(int i = 0; i < xs.size; i++) {
			string line = reader.ReadLine();
			// Error.Write($"Read line: \"{line}\"\n");
			string[] words = line.Split(' ');
			xs[i] = double.Parse(words[0]);
			ys[i] = double.Parse(words[1]);
			sigma_ys[i] = double.Parse(words[2]);
		}
	}

}


