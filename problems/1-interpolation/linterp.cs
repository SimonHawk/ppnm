using static System.Console;

public static class linInterp {
    public static double linterp(vector x, vector y, double z) {
        int i = search.binary_search(x, z);
        return y[i] + ((y[i+1]-y[i])/(x[i+1]-x[i])*(z-x[i]));
    }
    
    public static double linterpInteg(vector x, vector y, double z) {
        int iz = search.binary_search(x, z);
        double integral = 0.0; 
        for(int i = 0; i < iz; i++) {
            double delta_xi = x[i+1] - x[i];
            double pi = (y[i+1] - y[i])/delta_xi;
            integral += y[i]*delta_xi + 1/2*pi*delta_xi*delta_xi;
        }
        double piz = (y[iz+1] - y[iz])/(x[iz+1] - x[iz]);
        integral += y[iz]*(z-x[iz]) + 1/2*piz*(z-x[iz])*(z-x[iz]);
        return integral;
    }
}

