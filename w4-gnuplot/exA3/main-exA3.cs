using static System.Console;

class main {
	static void Main() {
		double deltax = 1.0/32;
		double eps = 1.0/512;
		for(double x = -5; x<=5; x += deltax) {
			// Sample two close points for every point, thereby ensuring that
			// I sample close to the singularities, and makes the plot nice.
			Write("{0,15:f8} {1,15:f8}\n", x-eps, math.gamma(x-eps));			
			Write("{0,15:f8} {1,15:f8}\n", x+eps, math.gamma(x+eps));			
		}
	}
}
