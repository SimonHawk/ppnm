using static System.Math;

class main {
	// Reads the input file given in the first command line argument
	// args[0], calculates the cosine of the parsed number in each line
	// and saves these in the file given by the second command line argument
	static int Main(string[] args) {
		var inputFile = new System.IO.StreamReader(args[0]);
		var outputFile = new System.IO.StreamWriter(args[1], append:true);		
		string inputLine;
		while((inputLine = inputFile.ReadLine()) != null) {
			double inputNumber = double.Parse(inputLine);
			outputFile.WriteLine("Cos({0}) = {1}", inputNumber, Cos(inputNumber));
		}

		outputFile.Close();
		return 0;
	}	
}
