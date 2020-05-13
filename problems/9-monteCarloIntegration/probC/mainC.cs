using static System.Math;
using static System.Console;
using System;

class mainC {

	static void Main() {
		Write("Problem C:\n");
		Func<vector, double> f0 = (v) => v[0];
		vector a0 = new vector(0.0);
		vector b0 = new vector(1.0);
		int N0 = 10000;
		
		double[] result0 = mcIntegrator.stratisfiedmc(f0, a0, b0, N0);
		double expRes0 = 0.5;

		Write("Completed!\n");		
		Write($"Integrating f(x,y) = y by stratified monte carlo:\n");
		Write($"a0:                {a0}\n");
		Write($"b0:                {b0}\n");
		Write($"Q0:                {result0[0]}\n");
		Write($"error:            {result0[1]}\n");
		Write($"Deviation:        {result0[0]-expRes0}\n");

		
		Func<vector, double> f2 = (v) => Sin(v[0])*Sin(v[0]);
		vector a2 = new vector(0.0);
		vector b2 = new vector(2*PI);
		int N2 = 1000;
		
		double[] result2 = mcIntegrator.stratisfiedmc(f2, a2, b2, N2);
		double expRes2 = PI;
				

		Write($"\nIntegrating Sin(x)^2 by stratified monte carlo:\n");
		Write($"a2:                {a2}\n");
		Write($"b2:                {b2}\n");
		Write($"Q:                 {result2[0]}\n");
		Write($"Error:             {result2[1]}\n");
		Write($"Deviation:         {result2[0]-expRes2}\n");
		

		// Now for Dimitris function:
		Func<vector, double> f3 = (v) =>
			1/(1-Cos(v[0])*Cos(v[1])*Cos(v[2]))*1/(PI*PI*PI);
		vector a3 = new vector(0.0, 0.0, 0.0);
		vector b3 = new vector(PI, PI, PI);
		int N3 = 10000;
		double expRes3 = 1.3932039296856768591842462603255;
		double[] result3 = mcIntegrator.stratisfiedmc(f3, a3, b3, N3);

		Write($"\nIntegrating 1/(1-cos(x)cos(y)cos(z))*1/(Pi*Pi*Pi) by stratified monte carlo:\n");
		Write($"a3:                {a3}\n");
		Write($"b3:                {b3}\n");
		Write($"Q:                 {result3[0]}\n");
		Write($"Error:             {result3[1]}\n");
		Write($"Deviation:         {result3[0]-expRes3}\n");
		

		// Make plot to compare with plain monte carlo:
		var outfile = new System.IO.StreamWriter("out.dataC.txt");
		int minN = 1000;
		int maxN = 50000;
		int points = 100;
		for(int N4 = minN; N4 <= maxN; N4 += (maxN-minN)/points) {
			double[] strat_result4 = mcIntegrator.stratisfiedmc(f2, a2, b2, N4);
			double[] plain_result4 = mcIntegrator.plainmc(f2, a2, b2, N4);
			outfile.Write("{0} {1} {2} {3} {4}\n", N4, plain_result4[1], strat_result4[1], Abs(plain_result4[0]-expRes2), Abs(strat_result4[0]-expRes2));
			
		}

		// Make map of where the points are sampled:
		// Seperate the data in a new section:
		outfile.Write("\n\n");

		// Define circular function that also writes the sampled points to
		// the data file:
		Func<vector, double> f5 = (v) => {
			outfile.Write("{0}\n", v);
			if((v[0]*v[0] + v[1]*v[1]) < 1) return 1;
			else return 0;
		};
		int N5 = 100000;
		vector a5 = new vector(-1.4, -1.4);
		vector b5 = new vector(1.4, 1.4);
		mcIntegrator.stratisfiedmc(f5, a5, b5, N5);
		
	     



	}

}
