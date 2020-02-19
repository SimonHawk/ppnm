using System;

class cmdline {
	static int Main() {
		System.IO.TextReader stdin = System.Console.In;
		System.IO.TextWriter stdout = System.Console.Out;
		System.IO.TextWriter stderr = System.Console.Error;
		System.IO.StreamReader infile = new System.IO.StreamReader("input.txt");
		System.IO.StreamWriter outfile = new System.IO.StreamWriter("output.txt", append:false);

		string s;
		do {
			// Console.ReadLine() calls Console.In.Readline(), and is the
			// starndard input
			s = Console.ReadLine();
			if(s==null)break;
			string[] words = s.Split(' ', ',', '\t');
			foreach(var word in words) {
				double x = double.Parse(word);
				// Console.WriteLine() calls Console.Out.WriteLine()
				// and writes to the starndard output.
				// Console.WriteLine("{0} {1} {2}", x,  Math.Sin(x), Math.Cos(x));
				stdout.WriteLine("{0} {1} {2}", x,  Math.Sin(x), Math.Cos(x));
				stderr.Error.WriteLine("{0} {1} {2}", x,  Math.Sin(x), Math.Cos(x));
				outfile.Error.WriteLine("{0} {1} {2}", x,  Math.Sin(x), Math.Cos(x));
			}
		}
		while(true);
		outfile.close();
		return 0;
	}
}
