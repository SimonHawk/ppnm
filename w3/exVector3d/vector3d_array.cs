public struct vector3d_array : ivector3d  {
	private double[] vec;
	
	public double x{ get{return vec[0];} set{vec[0] = value;}}
	public double y{ get{return vec[1];} set{vec[1] = value;}}
	public double z{ get{return vec[2];} set{vec[2] = value;}}

	public vector3d_array(double a, double b, double c) {
	    vec = new double[3];
		x = a;
		y = b;
		z = c;
	}
	
	public static vector3d_array operator+(vector3d_array v, vector3d_array u) {
		return new vector3d_array(v.x + u.x, v.y + u.y, v.z + u.z);
	}

	public static vector3d_array operator-(vector3d_array v, vector3d_array u) {
		return new vector3d_array(v.x - u.x, v.y - u.y, v.z - u.z);
	}

	public static vector3d_array operator*(double a, vector3d_array v) {
		return new vector3d_array(a*v.x, a*v.y, a*v.z);
	}
		
	public static vector3d_array operator*(vector3d_array v, double a) {
		return a*v;
	}

	public double dot_product(ivector3d other) {
		return x*other.x + y*other.y + z*other.z;
	}
	
	public ivector3d cross_product(ivector3d other) {
		double i = y * other.z - z * other.y;
		double j = z * other.x - x * other.z;
		double k = x * other.y - y * other.x;
		return new vector3d_array(i, j, k);
	}
	
	public double magnitude() {
		return System.Math.Sqrt(x*x + y*y + z*z);
	}
	
	public override string ToString(){
		return string.Format("[{0:f4}, {1:f4}, {2:f4}]", x, y, z);
	}

}
