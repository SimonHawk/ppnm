using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System;

class mainB {
// Finding the figure 8 stable 3 body netownian gravity problem.
// Assumptions:
// 1: All three bodies are confined to a plane, and each position can be described by
// 	  3d vector
// 2: The masses of the three bodies are the same. It seems that this is a prerequisite
//    for the solution to exist.	

	static void Main() {
		// The differential equation is defined in the method: make3BodyEq().
		
		// First define the constants:
		double m = 1.0;
		double G = 1.0;
			
		// Setup the initial vector y, as described in the Montgomery article:
		vector r1_0 = new vector(new double[] {0.97000436, -0.24308753});
		vector r2_0 = -r1_0.copy();
		vector r3_0 = new vector(new double[] {0, 0});
		vector dr3_0 = new vector(new double[] {-0.93240737, -0.86473146});
		vector dr2_0 = -dr3_0.copy()/2;
		vector dr1_0 = dr2_0.copy();
	
		vector y0 = new vector(new double[] 
			{r1_0[0], r1_0[1], r2_0[0], r2_0[1], r3_0[0], r3_0[1],
			 dr1_0[0], dr1_0[1], dr2_0[0], dr2_0[1], dr3_0[0], dr3_0[1]});
	
		double t0 = 0;
		double tb = 100;

	    make3BodyEq(m*G)(t0, y0).print("y' at t0: ");
		
		double acc = 1e-5;
		double eps = 1e-5;
		double h = 1e-3;
		
		var ts = new List<double>();
		var ys = new List<vector>();

		int steps = rkode.rk45(make3BodyEq(m*G), t0, ref y0, tb, h, acc, eps, ts, ys);		
		
		Write($"Done in {steps} steps!\n");
		
		var outfile = new System.IO.StreamWriter("out.probC.txt");

		for(int i = 0; i < ys.Count; i++) {
			outfile.Write("{0} {1} {2} {3} {4} {5} {6}\n", ts[i], ys[i][0], ys[i][1], ys[i][2], ys[i][3], ys[i][4], ys[i][5]);		
		}
		outfile.Close();

	}

	// Function to generate the 3 body equation:...
	// mG is the product of the equal mass of the 3 bodies and the gravitational constant.
	// Careful, the result is a 12 entry vector...
	public static Func<double, vector, vector> make3BodyEq(double mG) {
			return (x, y) => {
				vector r1 = new vector(new double[] {y[0], y[1]});
				vector r2 = new vector(new double[] {y[2], y[3]});
				vector r3 = new vector(new double[] {y[4], y[5]});
				
				// Write out the second derivatives:
				vector ddr1 = -mG*(r1-r2)/(Pow((r1-r2).norm(),3)) - mG*(r1-r3)/(Pow((r1-r3).norm(),3));
				vector ddr2 = -mG*(r2-r3)/(Pow((r2-r3).norm(),3)) - mG*(r2-r1)/(Pow((r2-r1).norm(),3));
				vector ddr3 = -mG*(r3-r1)/(Pow((r3-r1).norm(),3)) - mG*(r3-r2)/(Pow((r3-r2).norm(),3));
				
				vector dy = new vector(new double[] {y[6], y[7], y[8], y[9], y[10], y[11], ddr1[0], ddr1[1], ddr2[0], ddr2[1], ddr3[0], ddr3[1]});
				return dy;
			};
		}
	
}
