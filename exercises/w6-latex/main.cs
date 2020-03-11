using static System.Console;

class main {
	static void Main() {
		double delta_t = 0.01;
		for(double t = -10; t <= 10; t += delta_t) {
			Write("{0:f16} {1:f16} {2:f16}\n", t, funcs.S(t), funcs.C(t));
		}
	}
}
