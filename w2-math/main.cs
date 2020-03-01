using static cmath;
using static System.Console;
using static System.Math;

class main {
	static void Main() {
		exerciseA();
		// Exercise B: I don't know how to calculate complex squre root
		// Dimitri has defined a method for abs that I don't get yet.
		exerciseB();	
	}

	static void exerciseA() {
		// Exercise: Calculate a bunch of stuff:
		Write("Math Exercise A:\n");
		// Sqrt(2):
		double x = 2;
		Write($"sqrt(2): {sqrt(x)}\n");

		// e^i
		complex I = new complex(0, 1);
		Write($"e^i: {exp(I)}\n");
		
		// e^(i*PI)
		Write($"e^(i*pi): {exp(I*PI)}\n");
		
		// i^i
		Write($"i^i: {I.pow(I)}\n");

		// sin(i*pi)
		Write($"sin(i*pi): {sin(I*PI)}\n");
		
		// Just another write for nice formatting of output:
		Write("\n");
	}

	static void exerciseB() {
		// Exercise B: Implement new methods in cmath.cs and test
		Write("Math Exercise B\n");
		var I = new complex(0, 1);
		
		// sinh(i);
		Write($"sinh(i): {sinh(I)}\n");

		// cosh(i); 
		Write($"cosh(i): {cosh(I)}\n");
		
		// sqrt(-1)
		Write($"sqrt(-1): {sqrt((complex)(-1))}\n");

		// sqrt(i);.
		Write($"sqrt(i): {sqrt(I)}\n");

	}
}	
