using static System.Console;
using static System.Math;

class main {
	static void Main() {
		vector v = new vector(0.0, 2.0, 4.0, 6.0);
		int idx = search.binary_search(v, 5.999);

		Write($"The found index is: {idx}");
	}
}

static class linInterp {
	public static double linterp(vector x, vector y, double z) {
		int i = search.binary_search(x);
		return y[i] + ((y[i+1]-y[i])/(x[i+1]-x[i])*(z-x[i]))
	}

	public static double linterpInteg(vector x, vector y, double z) {
		int iz = search.binary_search(x);
		double integral;
		for(int i = 0; i < iz; i++) {
			double delta_xi = x[i+1] - x[i];
			double pi = (y[i+1] - y[i])/delta_xi;
			integral += y[i]*delta_xi + 1/2*pi*delta_xi*delta_xi;
		}
		double piz = (y[iz+1] - y[iz])/(x[iz+1] - x[iz]);
		integral += y[iz]*(z-x[iz]) + 1/2*piz*(z-x[iz])(z-x[iz]);
		return integral;
	}
}

class search {
	// Returns the index of the nearest smaller to z in the vector v.
	// That is: returns idx, z is in the range [v[idx], v[idx+1]]
	public static int binary_search(vector v, double z) {
		int L = 0;
		int R = v.size-1;
		int m;
		while(L<=R) {
			m = (int) Floor((L+R)/2.0);
			if(v[m] < z) {
				L = m + 1;
			} else if(v[m] > z) {
				R = m - 1;
			}
			if(L == (R+1)) {
				return L;
			}
		}
		// Return -1 (false index) if the algorith failed
		return -1;
	}
}
	
