public class vector_field : avector {
	private double _x, _y, _z;
	
	override public double x{ get{return _x;} set{_x = value;}}
	override public double y{ get{return _y;} set{_y = value;}}
	override public double z{ get{return _z;} set{_z = value;}}
	
	public vector_field(double a, double b, double c) {
		x = a;
		y = b;
		z = c;
	}

}

