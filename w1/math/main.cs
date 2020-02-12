using System;
using static System.Console;
using static System.Math;

class main {
	static int Main(){
		// Floats no longer exist, because the processer works directly on
		// doubles.

		// C# is statically typed, and a variable can only contain
		double x = 1;
		Write("sin({0})={1}\n", x, Sin(x));
		Write($"sin({x})={Sin(x)}\n");

		// Other types:
		// Single quotes for char
		char c = 'ø';
		// double quotes for string
		string s = "hello";

		double y = x*Exp(x);
		Write($"y = {y}, E = {E}\n");

		return 0;
	}
}

