using static System.Console;

class main {
	static void Main() {
		double deltax = 1.0/16;
		double eps = 1.0/64;
		for(double x = -5+eps; x<=5; x += deltax) {
			Write("{0,15:f8} {1,15:f8}\n", x, math.lngamma(x));			
		}
	}
}
