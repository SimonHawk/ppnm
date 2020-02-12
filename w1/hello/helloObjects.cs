class hello {
	static int Main() {
		System.Console.Write("Hello, World!\n");
		Test test = new Test(42);
		System.Console.Write(test.printNumber());
		System.Console.Write("\n");
		return 0;
	}
}

class Test {
	int number;
	
	public Test(int num) {
		this.number = num;
	}

	public int printNumber() {
		System.Console.Write($"Number + 2: {number+2}\n");
		return number;
	}

}
