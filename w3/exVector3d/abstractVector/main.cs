using static System.Console;

class main {
	static void Main() {
		avector v = new vector_field(1, 2, 3);
		vector_field v2 = new vector_field(1, 2, 3);
		Write("v = {0}\n", v);
		Write("v2 = {0}\n", v2);

		Write("v + v2 = {0}\n", v + v2);
		
	}
}
