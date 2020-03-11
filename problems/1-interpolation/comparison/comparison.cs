using static System.Console;

class comparison {
	static void Main() {
		int NPoints = 10;
		vector xs = new vector(NPoints);
		vector ys = new vector(NPoints);
		int i = 0;
		double xa = -4.5;
		double xb = 4.5;
		double delta_x = (xb-xa)/(NPoints - 1);
		for(double x = xa; x <= xb; x+=delta_x) {
			xs[i] = x;
			ys[i] = 1.0/(1.0+x*x);
			Error.Write("{0} {1}\n", x, ys[i]);
			i++;
		}
		
		qspline qspliner = new qspline(xs, ys);
		cspline cspliner = new cspline(xs, ys);

		for(double z = xa; z < xb; z+=0.05) {
			double linterp = linInterp.linterp(xs, ys, z);
			double qinterp = qspliner.spline(z);
			double cinterp = cspliner.spline(z);
			Write("{0} {1} {2} {3}\n", z, linterp, qinterp, cinterp); 
		}		
	}	
}
