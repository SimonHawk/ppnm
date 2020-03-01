using static System.Console;

class main {
	static void Main() {
		vector3d v = new vector3d(1, 2, 3);
		Write("v = {0}\n", v);
		
		ivector3d iv = new vector3d(4, 5, 6);
		Write("iv = {0}\n", iv);
	
		ivector3d iv2 = new vector3d_array(7, 8, 9);
		Write("iv2 = {0}\n", iv2);

		iv2 = v.cross_product(iv);
		Write("iv2 = v x iv = {0}\n", iv2);
	}

}


