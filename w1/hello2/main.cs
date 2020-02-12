class hello {
	// This variable can be seen by everything in the class scope.
	// Variables can be shadowed in everything but blocks
	double x = 0;

	// There must be a static method:
	static int Main() {
		// Just to make a simple program:
		// Console is a static class. No need to make objects, but 
		// The variables will be the same.
		System.Console.Write("Hello\n");
		// Because returntype is int we need to return something
		// 0 usually means that everything is fine. 
		return 0;
	}
	
}
