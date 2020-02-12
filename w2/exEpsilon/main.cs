using static System.Console;
using static Approx;
using System;

class main {
	static void Main() {
		exercise_A();	
		exercise_B();
		exercise_C();
		exercise_D();
	}

	static void exercise_A() {
		Write($"Exercise A, the loops might take a while\n");
		int i = 0;
		while(i+1 > i) {i++;}
		Write($"My max int = {i}\n");
		
		i = 0;

		while(i-1 < i) {i--;}		
		Write($"My min int = {i}\n\n");
	}

	static void exercise_B() {
		Write($"Exercise B\n");
		// While 1+x is not equal to 1, devide x by 2. after last devision
		// multiply by two to go back to the previous step
		double x=1; while(1+x != 1){x /= 2;} x *= 2;
		Write($"The 'Double' machine epsilon is: {x}\n");
		Write($"(It should be {System.Math.Pow(2, -52)})\n\n");
		
		float y = 1F; while((float)(1F + y) != 1F){y /= 2F;} y *= 2F;
		Write($"The 'Float' machine epsilon is: {y}\n");
		Write($"(It should be {System.Math.Pow(2, -23)})\n\n");
	}

	static void exercise_C() {
		// i: Calculate the harmonic sums.
		Write($"Exercise_C\n");
		int max = int.MaxValue/10;
		float float_sum_up = 0;
		for(int i = 1; i < max; i++){float_sum_up += 1F/i;}
		Write($"Float sum up = {float_sum_up}\n");			
		
		float float_sum_down = 0;
		for(int i = max; i > 0; i--){float_sum_down += 1F/i;}
		Write($"Float sum down = {float_sum_down}\n");
		
		// ii: Explain the differenc of the two sums:
		// Must be roundoff error, because we in the first case we first
		// add "big numbers" and in the second case first add 
		// "small numbers"
		
		// Oppe fra og ned: Først lægger vi en masse store tal sammen
		// og til sidst i rækken skal vi lægge nogle små tal til det 
		// store tal en ad gangen. Dermed går de små tal tabt.
		// Nede fra sum: Før lægger vi en masse små tal sammen og det
		// kan vi godt gemme. Når vi så begynder at lægge større tal til
		// den totale sum, så er bidraget fra de små tal allerede 
		// rimeligt stort, og det giver et mindre tab af bidrag.:
		
		// iii: Does this sum converge as a functino of "max"?
		// It's the same for int.MaxValue and int.MaxValue/10 at least...

		// iv: No calculate it for the double type:
		double double_sum_up = 0D;
		for(int i = 1; i < max; i++){double_sum_up += 1D/i;}
		Write($"Double sum up = {double_sum_up}\n");
		
		double double_sum_down = 0D;
		for(int i = max; i > 0; i--){double_sum_down += 1D/i;}
		Write($"Double sum down = {double_sum_down}\n");
		
		// For the double sum it is much closer.		
	
	}
		
	static void exercise_D() {
		// Write a function [approx(...)] in a different file.
		// See approx.cs

		// Test of the method:
		Write("\nExercise D\n");
		var a = 1D;
		var b = a + 1e-11;
		Write($"Is a = {a} and b = {b} approx the same? {approx(a, b)}\n");	
		b = a + 1e-8;
		Write($"Is a = {a} and b = {b} approx the same? {approx(a, b)}\n");	
	}	
	
}
