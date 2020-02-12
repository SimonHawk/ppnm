public class vector {
	public int size{
		get{return data.Length;}
	}
	
	double[] data;

	public double this[int i] {
		get{return data[i];}
		set{data[i]=value;}
	}	
	
	public vector(int n) {
		data = new double[n];
	}

	public void print(string s="") {
		System.Console.Write(s);
		for(int i=0; i<size;i++) {System.Console.Write("{0:f3} ", this[i]);}
		System.Console.Write("\n");
	
	}	

	public static vector operator+(vector u, vector v) {
		vector r = new vector(u.size);
		for(int i = 0; i<u.size;i++) r[i] = u[i] + v[i];
		return r;
	}	

	public static vector operator*(double a, vector v) {
		vector r = new vector(v.size);
		for(int i = 0; i<v.size;i++) r[i] = a*v[i];
		return r;
	}

	public static vector operator*(vector v, double a) {
		return a*v;
	}



}
