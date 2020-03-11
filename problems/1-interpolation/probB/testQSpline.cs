using static System.Console;

class testQSpline {
	static void Main() {
		vector xs1 = new vector(new double[]{1, 2, 3, 4, 5});
		vector ys1 = new vector(new double[]{1, 1, 1, 1, 1});
		qspline qspliner1 =	new qspline(xs1, ys1);

		Write("For Constant function:\n");
		qspliner1.printParams();
		
		vector xs2 = new vector(new double[]{1, 2, 3, 4, 5});
		vector ys2 = new vector(new double[]{1, 2, 3, 4, 5});
		qspline qspliner2 =	new qspline(xs2, ys2);
		
		Write("\nFor Linear function:\n");
		qspliner2.printParams();

		vector xs3 = new vector(new double[]{1, 2, 3, 4, 5});
		vector ys3 = new vector(new double[]{1, 2*2, 3*3, 4*4, 5*5});
		qspline qspliner3 =	new qspline(xs3, ys3);
		
		Write("\nFor Quadratic function:\n");
		qspliner3.printParams();

	}
}
