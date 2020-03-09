using static System.Console;
using static System.Math;

public class dataMaker {
	static void Main() {
		double delta_x = 0.5;
		double xa = 0.0;
		double xb = 20.0;
		int N = (int) Floor((xb-xa)/delta_x);

		for(int i = 0; i<N; i++) {
			Write("{0:f16} {1:f16}\n", xa+i*delta_x, Sin(i*delta_x));
		}
	}
	
	public static vector[] readFileToVector(string filename) {
		var inputfile = new System.IO.StreamReader(filename);
		// Count the number of lines in the file:
		int NLines = 0;
		while(inputfile.ReadLine() != null) {
			NLines++;
		}
		// Reset the StreamReader:
		inputfile.Close();
		inputfile = new System.IO.StreamReader(filename);
		// Prepare vectors to be read:
		vector xs = new vector(NLines);
		vector ys = new vector(NLines);
		
		// Itterate over all lines in the fil :
		string line;
		int i = 0;
		while((line = inputfile.ReadLine()) != null) {
			// Split line on spaces:
			string[] words = line.Split(' ');
			// Parse the split line to doubles.
			xs[i] = double.Parse(words[0]);
			ys[i] = double.Parse(words[1]);
			i++;
		}
		// Collect result to be returned:
		vector[] result = new vector[]{xs, ys};
		
		return result;
	}

}
