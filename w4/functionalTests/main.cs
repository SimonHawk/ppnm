using static System.Console;

class main {
	static int Main() {
		System.Func<double, double> mySin = System.Math.Sin;
		Write($"mySin(3.14/2) = {mySin(3.14/2)}\n");

		System.Func<double, double> lambdaSin = x => System.Math.Sin(x);
		Write($"lambdaSin(3.14/2) = {lambdaSin(3.14/2)}\n");

 		System.Func<double, double> pow3 = pow_n(3);
		Write($"pow3(3) = {pow3(3)}\n");

		return 0;
	}
	
	static System.Func<double, double>  pow_n(int n) {
		System.Func<double, double> pow_fun = x => 
		{
			double result = 1; 
			for(int i = 0; i < n; i++) {result *= x;}
			return result;
		};

		return pow_fun;
	}
}
