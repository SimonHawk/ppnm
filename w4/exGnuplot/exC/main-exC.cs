using static System.Console;
using static cmath;

class main {
	static void Main() {
		double delta = 1.0/32;
		double eps = 1.0/64;
		double min = -4.5;
		double max = 4.5;
		complex z;
		for(double re = min+eps; re<=max; re += delta) {
			for(double im = min+eps; im<=max; im += delta) {
				z = new complex(re, im);
				Write("{0:f8} {1:f8} {2:f8}\n", re, im, math.absgamma(z));
			}
			Write("\n\n\n");
		}
	}
}
