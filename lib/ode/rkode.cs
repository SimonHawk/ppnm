using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System;

public class rkode {
// My class for runge-kutta ODE solvers
// Consists of:
//	- A genral driver with adaptive step sizes
//	- 2 Stepper method, for the Runge Kutta midtpoint (rk12)
//    and Runge-Kutta-Feldhelm (rk45) embedded methods.
// 	- Interface functions that uses these two stepper directly
// 	  to solve some system ordinary differential equations.

	public static vector[] rk12_step(
		Func<double, vector, vector> f,
		double t,
		vector yt,
		double h
	) {
		// Estimate yh, by middel point method (2nd order):
		vector k_0 = f(t, yt);
		vector k_12 = f(t + 0.5*h, yt+0.5*h*k_0);
		vector yh = yt + h*k_12;
		// Estimate error as difference between 1st order and 2nd order:
		vector err = (h*k_12 - h*k_0);
		return new vector[] {yh, err};
	}

	public static int rk12(
		Func<double, vector, vector> f,
		double a,
		ref vector y,
		double b,
		double h, // Starting step size
		double acc,
		double eps,
		List<double> ts=null,
		List<vector> ys=null
	) {
		return general_driver(rk12_step, f, a, ref y, b, h, acc, eps, ts, ys);
	}
	
	// Implementation of the RK45 (Runge Kutta Fehlberg) butcher tableu
	// found on wikipedia
	public static vector[] rk45_step(
		Func<double, vector, vector> f,
		double t,
		vector yt,
		double h
	) {
		vector k_1 = f(t, yt);
		vector k_2 = f(t+1.0/4*h, 
			yt + 1.0/4*k_1*h);
		vector k_3 = f(t+3.0/8*h, 
			yt + 3.0/32*k_1*h + 9.0/32*k_2*h);
		vector k_4 = f(t+12.0/13*h, 
			yt + 1932.0/2197*k_1*h - 7200.0/2197*k_2*h + 7296.0/2197*k_3*h);
		vector k_5 = f(t + 1*h,
			yt + 439.0/216*k_1*h - 8*k_2*h + 3680.0/513*k_3*h - 845.0/4104*k_4*h);
		vector k_6 = f(t + 1.0/2*h, 
			yt + -8.0/27*k_1*h + 2*k_2*h - 3544.0/2565*k_3*h + 1859.0/4104*k_4*h - 11.0/40*k_5*h);
		
		vector[] ks = new vector[] {k_1, k_2, k_3, k_4, k_5, k_6};
		vector b5s = new vector(new double[] {16.0/135, 0, 6656.0/12825, 28561.0/56430, -9.0/50, 2.0/55});
		vector b4s = new vector(new double[] {25.0/216, 0, 1408.0/2565, 2197.0/4104, -1.0/5, 0});
	
		vector yh = yt;
		vector err = new vector(yt.size);
		for(int i = 0; i < b5s.size; i++) {
			yh += h*b5s[i]*ks[i];
			err += h*(b5s[i] - b4s[i])*ks[i];
		}	
		return new vector[] {yh, err};
	}

	public static int rk45(
		Func<double, vector, vector> f,
		double a,
		ref vector y,
		double b,
		double h, // Starting step size
		double acc,
		double eps,
		List<double> ts=null,
		List<vector> ys=null
	) {
		return general_driver(rk45_step, f, a, ref y, b, h, acc, eps, ts, ys);
	}

	public static int general_driver(
		Func<Func<double, vector, vector>, double,
			vector, double, vector[]> stepper,
		Func<double, vector, vector> f,
		double a,
		ref vector y,
		double b,
		double h, // Starting step size
		double acc,
		double eps,
		List<double> ts=null,
		List<vector> ys=null
	) {
		// The current posistion, set to starting condition:
		double t = a;
		vector yt = y; // Not needed?
		if(ts!=null) ts.Add(t);
		if(ys!=null) ys.Add(yt);
		vector err = new vector(y.size);
		vector[] res;
		// Step untill less than one step is needed:
		int steps = 0;
		while(t < b - h) {
			steps++;
			//Write($"\nStep nr. {steps}, h = {h}\n");
			// Do the step:	
			res = stepper(f, t, y, h);
			yt = res[0];
			err = res[1];
			// Calculate the current step tolerance:
			vector tau_is = (acc + eps*yt.abs())*Sqrt(h/(b-a));

			// Asses if the error of the step is within the tolerance
			// and calculate the ratio of the tolerance and the error
			// for each entry.
			bool errAccepted = true;
			vector tolRatios = new vector(err.size); 
			for(int i  = 0; i < tau_is.size; i++) {
				if(Abs(err[i]) > Abs(tau_is[i])) errAccepted = false;
				tolRatios[i] = Abs(tau_is[i])/Abs(err[i]);
			}
			
			// Is error estimate lower than tolerance?
			// Then step is accepted, t and yt
			if(errAccepted) {
				// Update t and y:	
				t += h;
				y = yt;
				if(ts!=null) ts.Add(t);
				if(ys!=null) ys.Add(yt);
			} else {
				// Don't change t and yt.
			}
			// Update h: (vector.min() is a new method I implemented myself)
			double h_factor = Pow(tolRatios.min(), 0.25)*0.95;
			h = (h_factor < 2)?h_factor*h:2*h;
		}
		// Do the final step to reach b: (For now, there is a chance that
		// the error gets too large in this step... But test this code 
		// first)
		double h_final = b - t;
		res = stepper(f, t, y, h_final);
		yt = res[0];
		err = res[1];
		y = yt;
		if(ts!=null) ts.Add(t + h_final);
		if(ys!=null) ys.Add(yt);
		return steps;
	}
}
