using static System.Math;

class main {
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
