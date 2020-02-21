using static cmath;
using static System.Console;

class main {
	static void Main() {
		// test the linspace function:
		double[] testArray = linspace(0, 5, 6);
		Write("testArray: linspace(0, 5, 10) = {0}\n", ArrayToString(testArray));
		exerciseA2();
	}
	
	public static void exerciseA2() {
		var xs = linspace(-3, 3, 1000);
		var ys = erf(xs);	
		exportDatFile(xs, ys, "erf.dat");		
	}
	
	public static double[] erf(double[] xs) {
		double [] ys = new double[xs.Length];
		for(int i = 0; i < xs.Length; i++) {
			ys[i] = erf(xs[i]);
		}
		return ys;
	}

	public static double erf(double x) {
		if(x < 0) return -erf(-x);
		double[] a = {0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
		double t = 1/(1+0.3275911*x);
		double sum = t*(a[0] + t*(a[1] + t*(a[2] + t*(a[3] + t*a[4]))));
		return 1 - sum*exp(-x*x);		
	}
	
	// Helper method to make it easy to make arrays, in a syntax useful
	// for plotting:
	public static double[] linspace(double start, double stop, int num) {
		double increment = (stop-start)/(num-1);
		double[] array = new double[num];
		array[0] = start;
		for(int i = 1; i < num; i++) {
			array[i] = array[i-1] + increment;
		}
		return array;
	}
	
	// Helper method for printing arrays:
	public static string ArrayToString(double[] array) {
		string stringRep = "[";
		foreach(double x in array) {stringRep += string.Format("{0}, ", x);}
		stringRep += "]";
		return stringRep;
	}

	// Exports a data series to a .dat data file.
	public static void exportDatFile(double[] xs, double[] ys, string filename = "outfile.dat") {
		var outfile = new System.IO.StreamWriter(filename, append:true);	
		for(int i = 0; i < xs.Length; i++) {
			// Write("{i}: Now i write to outfile??\n", i);
			outfile.WriteLine("{0}\t{1}", xs[i], ys[i]);
		}
		outfile.Close();
	}

}
