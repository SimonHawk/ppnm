using static System.Console;
using static cmath;

class main {
	static void Main() {
		double delta = 1.0/64;
		double eps = 1.0/128;
		double min = -4.5;
		double max = 4.5;
		complex z;
		for(double re = min+eps; re<=max; re += delta) {
			for(double im = min+eps; im<=max; im += delta) {
				z = new complex(re, im);
				// Start of including the phase in the plot.
				complex gammaValue = math.gamma(z);
				Write("{0:f8} {1:f8} {2:f8} {3:f8}\n", re, im, abs(gammaValue), complex.argument(gammaValue));
			}
			Write("\n");
		}
	}
}
