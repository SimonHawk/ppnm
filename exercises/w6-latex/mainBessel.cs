using static System.Console;
using static System.Math;

class mainBessel {
	static void Main() {
		double N_points = 30;
		double x_max = 10;
		double delta_x = x_max/N_points;
		for(double x = 0.00001; x <= delta_x*N_points; x += delta_x) {
			Write("{0:f16} {1:f16} {2:f16}\n", x, funcs.j0(x), Sin(x)/x);
		}
	}
}
