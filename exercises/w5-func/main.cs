using static System.Math;
using static System.Console;
using System;


class main {
	const double inf = System.Double.PositiveInfinity;
	const double nan = System.Double.NaN;

	static void Main() {
		exA();
		exB();
		exC();

	}	
	
	static void exA() {
		Write("\nExercise A:\n");
		Func<double, double> fun1 = (x) => Log(x)/Sqrt(x);
		double result1 = quad.o8av(fun1, 0, 1);
		Write("int[0->1]ln(x)/sqrt(x) = {0:f16}\n", result1);
		
		Func<double, double> fun2 = (x) => (Exp(-x*x));
		double result2 = quad.o8av(fun2, -inf, inf);
		Write("int[-inf->inf](Exp(-x^2))  = {0:f16}\n", result2);
		
		// This is tricky:
		Func<double, double> result3 = (p) => {
			Func<double, double> fun3 = (x) => Pow((Log(1/x)), p);
			return quad.o8av(fun3, 0, 1);
		};
		for(int i = 0; i < 6; i++) {
			Write("int[0->1](ln(1/x)^p)(p={0}) = {1:f16}\n", i, result3(i));	
		}
	}
	
	static void exB() {
		Write("\nExercise B:\n");
		Func<double, double> fun1 = (x) => Sqrt(x)*Exp(-x);
		double result1 = quad.o8av(fun1, 0, inf);
		Write("int[0->inf](sqrt(x)*exp(-x)) = {0:f16}\n", result1);
		
		Func<double, double> fun2 = (x) => x*x/(Exp(x)-1);
		double result2 = quad.o8av(fun2, 0, inf);
		Write("int[0->inf](x^2/(exp(x)-1)) = {0:f16}\n", result2);
		
		Func<double, double> fun3 = (x) => Pow(x, -x);
		double result3 = quad.o8av(fun3, 0, 1);
		Write("int[0->1](x^-x) = {0:f16}\n", result3);

	}
	
	static void exC() {
		Write("\nExercise C\n");
		Func<double, double> E = (alpha) => {
			Func<double, double> psi_norm_square = (x) => Exp(-alpha*x*x);
			Func<double, double> hamilton_exp_fun = (x) => (-alpha*alpha*x*x/2 + alpha/2 + x*x/2)*Exp(-alpha*x*x);		
			double overlap = quad.o8av(psi_norm_square, -inf, inf);
			double hamilton_exp_value = quad.o8av(hamilton_exp_fun, -inf, inf);
			return hamilton_exp_value/overlap;			
		};
		Write("See ExC.svg for plot of E(alpha)\n");
		Write("E(1) = {0:f16}\n", E(1));		

		var outfile = new System.IO.StreamWriter("out.exC.txt");
		for(double alpha = 1/16; alpha <= 3; alpha+=1.0/32) {
			outfile.Write("{0:f16} {1:f16}\n", alpha, E(alpha));
		}
		outfile.Close();
	
	}

}
