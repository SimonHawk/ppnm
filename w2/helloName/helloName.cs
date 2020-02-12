class helloName {
	static void Main() {
		// System.Environment.UserName is the name of the user of the
		// thread that runs the program.
		System.Console.Write($"Hello, {System.Environment.UserName}!\n");
	}
}
