using static System.Console;

class mainBessel {
	static void Main() {
		double delta_x = 0.0000001;
		for(double x = 0; x <= delta_x*100; x += delta_x) {
			Write("{0:f16} {1:f16}\n", x, funcs.j0(x));
		}
	}
}
