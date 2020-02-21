using static System.Console;
using static cmath;

class main {
	static void Main() {
		double deltax = 1.0/512;
		double eps = 1.0/1024;
		complex z;
		for(double x = -5+eps; x<=5; x += deltax) {
			z = new complex(x, 0);
			Write("{0,15:f8} {1,15:f8}\n", x, math.lngamma(z).Re);			
		}
	}
}
