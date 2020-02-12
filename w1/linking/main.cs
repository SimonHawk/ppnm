// If we use things from system, we don't need to explicitly write it out
using System;
// Then we can use all functions in the class System.Console
using static System.Console;

class main{
	static int Main(){
		//This works because we have using System up top, and we have
		// no other Console class
		Console.Write("Hello from Console\n");
		// Directly from the static System.Console:
		Write("hello from Write\n");

		// Form world:
		world.print();
	
		return 0;
	}
}

