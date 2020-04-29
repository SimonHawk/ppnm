using static System.Console;
using static System.Math;
using System;

class mainA {

	static void Main() {
		Write("Problem B:\n");
		// Now for Dimitris function:
		Func<vector, double> f = (v) =>
			1/(1-Cos(v[0])*Cos(v[1])*Cos(v[2]))*1/(PI*PI*PI);
		vector a = new vector(0.0, 0.0, 0.0);
		vector b = new vector(PI, PI, PI);
		double expectedRes = 1.3932039296856768591842462603255;
		
		// Make data file:
		var outfile = new System.IO.StreamWriter("out.dataB.txt");
		
		int Nmin = 10;
		int Nmax = 10000;
		int Nstep = 100;
		
		for(int N = Nmin; N <= Nmax; N += Nstep) {
			vector fitResult = mcIntegrator.plainmc(f, a, b, N);
			outfile.Write("{0} {1} {2}\n", N, fitResult[1], Abs(fitResult[0]-expectedRes));
		}
		outfile.Close();

	}
	
	public static void testFunction(Func<vector, double> f, vector a, vector b, int N, double res) {
		// Do the integral:
		vector intResult = mcIntegrator.plainmc(f, a, b, N);
		
		Write($"Lower limit:                      {a}\n");
		Write($"Upper limit:                      {b}\n");
		Write($"Number of samples:                {N}\n");
		Write($"Integration result:               {intResult[0]}\n");
		Write($"Integration error:                {intResult[1]}\n");
		Write($"Deviation from analytical result: {intResult[0]-res}\n");
	} 
}

