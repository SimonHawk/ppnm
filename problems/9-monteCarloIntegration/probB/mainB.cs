using static System.Console;
using static System.Math;
using System;

class mainA {

	static void Main() {
		Write("Problem B:\n");
		Write("See B.sigmaNDependence.svg\n");
		// Now for Dimitris function:
		/*
		Func<vector, double> f = (v) =>
			1/(1-Cos(v[0])*Cos(v[1])*Cos(v[2]))*1/(PI*PI*PI);
		vector a = new vector(0.0, 0.0, 0.0);
		vector b = new vector(PI, PI, PI);
		double expectedRes = 1.3932039296856768591842462603255;
		*/
		Func<vector, double> f = (v) => Sin(v[0])*Sin(v[0]);
		vector a = new vector(0.0);
		vector b = new vector(2*PI);
		double expectedRes = PI;
		
		// Make data file:
		var outfile = new System.IO.StreamWriter("out.dataB.txt");
		
		int Nmin = 1000;
		int Nmax = 50000;
		int Nstep = (Nmax - Nmin)/100;
		
		vector intResult0 = mcIntegrator.plainmc(f, a, b, Nmax);
		Func<double, double> simpleFit = (N) => intResult0[1]*Sqrt(Nmax)/Sqrt(N);

		for(int N = Nmin; N <= Nmax; N += Nstep) {
			vector intResult = mcIntegrator.plainmc(f, a, b, N);
			outfile.Write("{0} {1} {2} {3}\n", N, intResult[1], Abs(intResult[0]-expectedRes), simpleFit(N));
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

