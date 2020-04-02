using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System;

public class rkode {

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
		vector err = h*(k_12 - k_0);
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
		vector k_1 = h*f(t, yt);
		vector k_2 = h*f(t+1.0/4*h, 
			yt + 1.0/4*k_1);
		vector k_3 = h*f(t+3.0/8*h, 
			yt + 3.0/32*k_1 + 9.0/32*k_2);
		vector k_4 = h*f(t+12.0/13*h, 
			yt + 1932.0/2197*k_1 - 7200.0/2197*k_2 + 7296.0/2197*k_3);
		vector k_5 = h*f(t + 1*h,
			yt + 439.0/216*k_1 - 8*k_2 + 3680.0/513*k_3 - 845.0/4104*k_4);
		vector k_6 = h*f(t + 1.0/2*h, 
			yt + -8.0/27*k_1 + 2*k_2 - 3544.0/2565*k_3 + 1859.0/4104*k_4 - 11.0/40*k_5);
		
		vector b5 = 16.0/135*k_1 + 0*k_2 + 6656.0/12825*k_3 + 28561.0/56430*k_4 - 9.0/50*k_5 + 2.0/55*k_6;
		vector b4 = 25.0/216*k_1 + 0*k_2 + 1408.0/2565*k_3 + 2197.0/4104*k_4 - 1.0/5*k_5 + 0*k_6;
		
		vector err = b5-b4;
		vector yh = yt + b5;
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
		// Func<Func<double, vector, vector>, double, vector, double, vector, vector, int> stepper = rk45_step;
		// Func<Func<double, vector, vector>, double, vector, double, ref vector, ref vector, double> stepper; // = rk45_step;
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
			Write($"\nStep nr. {steps}, h = {h}\n");
			// Do the step:	
			res = stepper(f, t, y, h);
			yt = res[0];
			err = res[1];
			// stepper(f, t, y, h, ref yt, ref err);
			//yt.print($"yt = ");
			//Write($"err = [{err[0]}, {err[1]}]\n");
			//Write($"|yt| = {yt.norm()}\n");
			// Calculate the current step tolerance:
			//double tau_i = (acc + eps*yt.norm())*Sqrt(h/(b-a));
			vector tau_is = (acc + eps*yt.abs())*Sqrt(h/(b-a));
			tau_is.print("tau_is: ");
			err.print("err: ");
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
				Write($"New t: {t}, new h: {h}\n");
				//y.print("y at end of step: ");
				//Write("t and y changes!\n");
			} else {
				// Don't change t and yt.
				Write("t and y doesn't change...\n");
			}
			// Update h:
			Write($"tolRatios.min(): {tolRatios.min()}\n");
			double h_factor = Pow(tolRatios.min(), 0.25)*0.95;
			// Write($"tau_i/err.norm(): {tau_i/err.norm()}\n");
			h = (h_factor < 2)?h_factor*h:2*h;
			//Write($"New h: {h}\n");
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
		// Write($"Done in {steps} steps\n");
		return steps;
	}
}
