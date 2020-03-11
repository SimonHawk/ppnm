using static System.Math;
using static System.Console;

class mainA {
	static void Main(string[] args) {
		vector[] testData = dataMaker.readFileToVector("out.testDataA.txt");
		vector xs = testData[0];
		vector ys = testData[1];
		
		double xa = xs[0];
		double xb = xs[xs.size-1];
		double delta_z = 0.01;
		for(double z = xa; z < xb; z += delta_z) {
			double integral = linInterp.linterpInteg(xs, ys, z);
			double interp = linInterp.linterp(xs, ys, z);
			Write("{0:f16} {1:f16} {2:f16}\n", z, interp, integral);
		}
		
	}
}
