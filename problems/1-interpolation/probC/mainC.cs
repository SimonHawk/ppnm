using static System.Console;

class mainB {
	static void Main(string[] args) {
		vector[] testData = dataMaker.readFileToVector(args[0]);
		vector xs = testData[0];
		vector ys = testData[1];
		
		cspline cspliner = new cspline(xs, ys);
		
		double xa = xs[0];
		double xb = xs[xs.size-1];
		double delta_z = 0.01;
		for(double z = xa; z < xb; z+=delta_z) {
			double interp = cspliner.spline(z);
			double derivative = cspliner.derivative(z);
			double integral = cspliner.integral(z);
			Write("{0:f16} {1:f16} {2:f16} {3:f16}\n", z, interp, derivative, integral);
		}
		
	}

}
