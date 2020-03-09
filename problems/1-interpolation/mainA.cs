using static System.Math;
using static System.Console;

class mainA {
	static void Main(string[] args) {
		/*
		double delta_x = 0.5;
		double xa = 0.0;
		double xb = 20.0;
		int N = (int) Floor((xb-xa)/delta_x);
		vector xs = new vector(N);
		vector ys = new vector(N);

		for(int i = 0; i<N; i++) {
			xs[i] = xa+i*delta_x;
			ys[i] = Sin(i*delta_x);			
		}
		*/
		
		vector[] testData = dataMaker.readFileToVector("testDataA.txt");
		vector xs = testData[0];
		vector ys = testData[1];
		
		double xa = xs[0];
		double xb = xs[xs.size-1];
	 	/* Debug:	
		Write("xs: {0}\nys: {1}\n", xs.size, ys.size);
		Write($"Search 3.0: {search.binary_search(xs, 3.0)}\n");
		Write($"Search 3.1: {search.binary_search(xs, 3.1)}\n");
		Write($"Search 9.9: {search.binary_search(xs, 9.9)}\n");
		Write($"Search 2.0: {search.binary_search(xs, 2.0)}\n");
		Write($"Search 10.0: {search.binary_search(xs, 10.0)}\n");
		*/
		double delta_z = 0.01;
		for(double z = xa; z < xb; z += delta_z) {
			double integral = linInterp.linterpInteg(xs, ys, z);
			double interp = linInterp.linterp(xs, ys, z);
			Write("{0:f16} {1:f16} {2:f16}\n", z, interp, integral);
		}
		
	}
}
