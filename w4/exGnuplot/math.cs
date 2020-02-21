using static System.Math;
using static cmath;

public class math {
	public static double erf(double x){
		/// single precision error function (Abramowitz and Stegun, from Wikipedia)
		if(x<0) return -erf(-x);
		double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
		double t=1/(1+0.3275911*x);
		double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));/* the right thing */
		return 1-sum*Exp(-x*x);
	} 

	public static double gamma(double x){
		// Single precision gamma function (Gergo Nemes, from Wikipedia)
		if(x<0)return PI/Sin(PI*x)/gamma(1-x);
		if(x<9)return gamma(x+1)/x;
		return Exp(lngamma(x));
	}
	
	public static double lngamma(double x) {
		// I really hope this is correct, I just too a guess taking the
		// natural logarith of the previous statement:
		if(x<0) return Log(PI) - Log(Sin(PI*x)) - lngamma(1-x);
		if(x<9) return lngamma(x+1) - Log(x);
		return x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
	}
	
	public static complex lngamma(complex z) {
		// Trust the implicit conversion from double to 
		if(z.Re < 0) return log(PI) - log(sin(PI*z)) - lngamma(1.0 - z);
		if(z.Re < 9) return lngamma(z+1.0) - log(z);
		return z*log(z+1.0/(12.0*z - 1.0/z/10.0)) - z + log(2*PI/z)/2;
	}
}
