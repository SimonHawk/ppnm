using static System.Console;

class main {
	static void Main() {
		double deltax = 1.0/16;
		for(double x = -3; x<=3; x += deltax) {
			Write("{0,15:f8} {1,15:f8}\n", x, math.erf(x));			
		}
	}
}
