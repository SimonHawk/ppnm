using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;
using static System.Math;

public static partial class funcs{
    public static double j0(double x) {
        double n = 0.0;
        Func<double, vector, vector> sphBesselFun = (double t, vector ys) =>     {
            double ys0 = ys[1];
            double ys1 = 1/(t*t) * (-1)*(2*t*ys[1] + (t*t - n*(n-1))*ys[0]);
            return new vector(ys0, ys1);
        };
        double a = 0;
        double b = x;
        vector ya = new vector(1.0, 0.0);
 		
		vector yb = ode.rk23(sphBesselFun, a, ya, b);
		
        return yb[0];
    }
}
