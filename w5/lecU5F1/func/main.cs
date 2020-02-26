using System;
using static System.Console;
using static System.Math;


class main {
	
	delegate double myfun(double x);
	


	// This is a function that returns a function:
	static Func<double, double> makefun(double y) {
		double a;
		a = y;

		// Old school way to do it
		// return delegate(double x) {return y;};
		// By lambda expression:
		return x => a;

	}

	static double eval(Func<double, double> f, double x) {
		return f(x);
	}

	static Func<double, Func<double, double>> makemakefun(double y) {
		Func<double, Func<double, double>> result = (double x) => ((t)=>x);
		return result;
	}
	
	int ncalls;
	static double gamma(double z) {
		const double inf=System.Double.PositiveInfinity;
		const double nan=System.Double.NaN;
	
		// Using eulers reflection formula for the negatives:
		if(z<0) return -PI/Sin(PI*z)/(gamma(1+z));
		// We want z to be somewhere between 2 and 3
		if(z<1) return gamma(z+1)/z;
		if(z>3) return gamma(z-1)*(z-1);
		Func<double, double> f = (x) => Pow(x,z-1)*Exp(-x);
		return quad.o8av(f, 0, inf, acc:1e-3, eps:0);
	}

	static int Main() {
		Func<double, double> f = delegate(double x) {return x;};
		Write("f({0}) = {1}\n", 9, f(9));
		// This will be a different type than f, because the delegate is
		// in our class, and the Func is in the system class
		myfun g = delegate(double x) {return x;};		

		
		Func<double, double> f1 = makefun(1);
		Func<double, double> f2 = makefun(2);
		
		double a;
		a = 1;
		Func<double, double> h1 = (x) => a;
		a = 2;
		Func<double, double> h2 = (x) => a;
		var f3 = makefun(2);
	
		Write($"f1(0) = {f1(0)}, f2(0) = {f2(0)}\n");	
		a = 9;	
		Write($"a = {a}, h1(0) = {eval(h1, 0)}, h2(0) = {eval(h2, 0)}\n");		
		for(double x = 0.5; x < 9; x+=0.5) {
			Write($"{x:f5} {gamma(x):f15}\n");
			
		}		

		return 0;
	}	

}
