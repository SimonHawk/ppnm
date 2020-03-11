using static System.Console;

class main {
	static void Main(string[] args) {
		if(args.Length == 0) {
			Error.Write("No command-line arguments given!\n");

		} else {
			for(int i=0; i < args.Length; i++) {
				double arg = double.Parse(args[i]);
				Write("Cos({0:f5}) = {1:f5}\n", arg, System.Math.Cos(arg));
			}
		}
	}	

}
