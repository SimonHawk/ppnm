using System;

public static class leastSquares {
	
	public static vector calculateC(data dat, Func<double, double>[] fs) {
		matrix A = calculateA(dat, fs);
		vector b = calculateb(dat);
		qr_gs solver = new qr_gs(A);
		vector c = solver.solve(b);
		return c;
	}

	public static matrix calculateSigma(data dat, Func<double, double>[] fs){
		matrix A = calculateA(dat, fs);
		qr_gs solver = new qr_gs(A.transpose()*A);
		matrix sigma = solver.inverse();
		return sigma;
	}
	
	static matrix calculateA(data dat, Func<double, double>[] fs) {
		int m = fs.Length;
		int n = dat.xs.size;
		matrix A = new matrix(n, m);
		for(int i = 0; i < n; i++) {
			for(int k = 0; k < m; k++) {
				A[i, k] = ( fs[k](dat.xs[i]) ) / dat.dys[i];
			}
		}
		return A;
	}
	
	static vector calculateb(data dat) {
		int n = dat.xs.size;
		vector b = new vector(n);
		for(int i = 0; i < n; i++) {
			b[i] = dat.ys[i]/dat.dys[i];
		}
		return b;
	}

}

// Small data class to contain the data to fit to.
public class data {
	public vector xs, ys, dys;
	
	public data(vector x, vector y, vector dy) {
		xs = x;
		ys = y;
		dys = dy;	
	}
	
	// This constructor reads a two column file, 1st column is xs, second is 	// ys, and calculates the dys as errorFunc(ys);
	public data(string filename, Func<double, double> errorFunc) {
		var reader = new System.IO.StreamReader(filename);
		int fileLength = 0;
		while(reader.ReadLine()!=null) {
			fileLength += 1;
		}
		reader.Close();
		xs = new vector(fileLength);
		ys = new vector(fileLength);
		dys = new vector(fileLength);
		reader = new System.IO.StreamReader(filename);
		for(int i = 0; i < xs.size; i++) {
			string line = reader.ReadLine();
			string[] words = line.Split(' ');
			xs[i] = double.Parse(words[0]);
			ys[i] = double.Parse(words[1]);
			dys[i] = errorFunc(ys[i]);
		}
	}
	
}
