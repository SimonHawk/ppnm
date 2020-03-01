public struct vector3d : ivector3d  {
	private double _x, _y, _z;
	
	public double x{ get{return _x;} set{_x = value;}}
	public double y{ get{return _y;} set{_y = value;}}
	public double z{ get{return _z;} set{_z = value;}}

	public vector3d(double a, double b, double c) {
		_x = a;
		_y = b;
		_z = c;
	}
	
	public static vector3d operator+(vector3d v, vector3d u) {
		return new vector3d(v.x + u.x, v.y + u.y, v.z + u.z);
	}

	public static vector3d operator-(vector3d v, vector3d u) {
		return new vector3d(v.x - u.x, v.y - u.y, v.z - u.z);
	}

	public static vector3d operator*(double a, vector3d v) {
		return new vector3d(a*v.x, a*v.y, a*v.z);
	}
		
	public static vector3d operator*(vector3d v, double a) {
		return a*v;
	}

	public double dot_product(ivector3d other) {
		return x*other.x + y*other.y + z*other.z;
	}
	
	public ivector3d cross_product(ivector3d other) {
		double i = y * other.z - z * other.y;
		double j = z * other.x - x * other.z;
		double k = x * other.y - y * other.x;
		return new vector3d(i, j, k);
	}
	
	public double magnitude() {
		return System.Math.Sqrt(x*x + y*y + z*z);
	}
	
	public override string ToString(){
		return string.Format("[{0:f4}, {1:f4}, {2:f4}]", x, y, z);
	}

}
