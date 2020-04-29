using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using System.Diagnostics;



class main {

	// Object is like a general parameter
	// We will make sure it is local to the function:
	public static void hsum(object o) {
		// Cast the object to data:
		data d = (data)o;
		d.sum=0;
		for(int i = d.a; i < d.b; i++) {
			d.sum += 1.0/i;
		}
		Write($"Partial hsum: from {d.a} to {d.b}: {d.sum}\n");
	}	

	static void Main(string[] args) {
		
		int N = (int)1e9;
		if(args.Length > 0) {
			// So you can input exponential notation
			N = (int)double.Parse(args[0]);
		}
		WriteLine($"N = {N}\n");
		data data1 = new data();
		data data2 = new data();

		data1.a = 1;
		data1.b = N/2;
		data2.a = N/2;
		data2.b = N;
		
		// Create a new thread from a delegate with return type void:
		Thread t = new Thread(hsum);
		// Start the thread using data1:
		// will run in independent thread:
		var watch = new Stopwatch();
		watch.Reset();
		watch.Start();
		t.Start(data1);
		// The other one joins in mail thread.
		hsum(data2);

		// Now join t to the main thread:
		t.Join();
		watch.Stop();
		
		double sum = data1.sum + data2.sum;
		
		Write($"Main hsum: from {1} to {N}: {sum}\n");
		Write($"Time: {watch.ElapsedMilliseconds/1000.0} sec\n");


		watch.Reset();
		watch.Start();
		data2.a = 1;
		data2.b = N;
		hsum(data2);
		watch.Stop();
		Write($"\nOne thread:: from {1} to {N}: {data2.sum}\n");
		Write($"Time: {watch.ElapsedMilliseconds/1000.0} sec\n");
		

		// Parallel for loop:
		// Over local double variable:
		// Look it up online...
		watch.Reset();
		watch.Start();
		data2.sum = 0;
		Parallel.For<double>(1, N, ()=>0.0, (i, loop, psum) => psum+=1.0/i, (psum)=>{lock(data2);data2.sum+=psum;});		
		watch.Stop();
		Write($"\n Parallel for: from {1} to {N}: {data2.sum}\n");
		Write($"Time: {watch.ElapsedMilliseconds/1000.0} sec\n");
		

	}
}

public class data {
	public int a, b;
	public double sum;	
}
