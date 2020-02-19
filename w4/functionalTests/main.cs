using static System.Console;

class main {
	static int Main() {
		Func<double, double> mySin = System.Math.Sin;
		Write($"mySin(3.14/2) = {mySin(3.14/2)}\n");
	}
}
