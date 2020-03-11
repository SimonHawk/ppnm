using static System.Console;

class main {
	static void Main() {
		// System.Console.In is the standard input stream.
		// Save it in an variable to reference later.
		System.IO.TextReader stdin = System.Console.In;
		string line;
		// This works because an asignment returns the asigned value:
		// If the read line is null, that means that there are no more
		// lines to read.
		while( (line = stdin.ReadLine()) !=null) {
			// Parse read string to a double. This will not work if the 
			// string can't be parsed, but then it will just throw an
			// exception, which is okay.
			double inputNumber = double.Parse(line);
			Write("Cos({0:f5}) = {1:f5}\n", inputNumber, System.Math.Cos(inputNumber));
		}
	
	}	

}
