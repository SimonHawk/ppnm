using static System.Console;

class main {
	static int Main() {
		double eps = 1.0/32;
		double dx = 1.0/16;
		for(double x=-4+eps; x <= 6; x+=dx) {
			WriteLine("{0,10:f3} {1,15:f8}", x, math.gamma(x));
		}	
		return 0;		

	} // Main

} // main
