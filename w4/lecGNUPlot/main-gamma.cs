using static System.Console;
using static System.Math;

class main {
	static int Main() {
		double eps = 1.0/32;
		double dx = 1.0/16;
		for(double x=-4+eps; x <= 6; x+=dx) {
			WriteLine("{0,10:f3} {1,15:f8}", x, math.gamma(x));
		}	
		double tg = 1;
		for(double x = 1; x <= 170; x+=1) {
			var gx = math.gamma(x);
			WriteLine("{0,10:f3} {1,15:f8} {2:}", x, Abs(gx-tg)/tg);
			tg = tg*x;
		}
		return 0;		

	} // Main

} // main
