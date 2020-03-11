using static cmath;

public class Approx {
	public static bool approx(double a, double b, double tau=1e-9, double epsilon=1e-9) {
		if (abs(a-b) < tau) {
			return true;
		} else if (abs(a-b)/(abs(a)+abs(b)) < epsilon/2) {
			return true;
		} else {
			return false;
		}
	}
}	
