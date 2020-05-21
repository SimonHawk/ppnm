using static System.Console;
using static System.Math;
using System;

// Class for the downhill simplex method:
public class simplex {
	Func<vector,double> phi; // The function to optimize.
	int n; // Dimmension of the space:
	vector[] verticies; // The points of the simplex:
	vector values; 
	int hi; // Index of the highest point
	int lo; // Index of the lowest point:

	vector centroid{get{
		vector sum = new vector(n); 
		for(int i = 0; i < n+1; i++) if(i!=hi) sum += verticies[i];
		return 1.0/n * sum;
	}} // The centroid, defined by a getter method.

	public double size{get{
		double sum = 0;
		for(int i = 0; i < n; i++) {
			sum += (verticies[i+1] - verticies[i]).norm();	
		}
		return sum;
	}}

	public vector minimum{get{return verticies[lo];}}
	
	public simplex(Func<vector, double> f, int dim) {
		phi = f;
		n = dim;
		verticies = new vector[n+1];
		for(int i = 0; i < n+1; i++) {
			verticies[i] = new vector(n);
		}
		values = new vector(n+1);
	}

	// Calculate the values of phi in the verticies.
	// Calls the function n+1 times... This is probably pretty slow.
	void calculateValues() {
		// There are n+1 verticies:
		for(int i = 0; i < n+1; i++) {
			//Error.Write($"{i}'th vertex: {verticies[i]}\n");
			//Error.Write($"Calculating {i}'th value\n");
			values[i] = phi(verticies[i]);
		}
	}

	// Method for updating the index of the highest and lowest point.
	// Doesn't call the function, so this is probably pretty quick.
	void updateHiLo() {
		int newLo = 0;
		int newHi = 0;
		for(int i = 0; i < n+1; i++) {
			if(values[i] < values[newLo]) newLo = i;
			if(values[i] > values[newHi]) newHi = i;
		}
		lo = newLo;
		hi = newHi;
	}

		
	public int search(
		vector startingPoint,  // A starting point:
		vector startingLengths, // The length that the other points will be
							   // moved in relation to the starting point.
		double eps             // The accuracy goal
	) {
		vector[] startingShape = new vector[n+1];
		for(int i = 0; i < n+1; i++) {
			startingShape[i] = startingPoint.copy();
			if(i != 0) startingShape[i][i-1] += startingLengths[i-1];
		}
		
		return search(startingShape, eps);
	}



	// Helper method to make setting up the starting shape easier:
	public int search(
		vector startingPoint,  // A starting point:
		double startingLength, // The length that the other points will be
							   // moved in relation to the starting point.
		double eps             // The accuracy goal
	) {
		vector[] startingShape = new vector[n+1];
		for(int i = 0; i < n+1; i++) {
			startingShape[i] = startingPoint.copy();
			if(i != 0) startingShape[i][i-1] += startingLength;
		}
		
		return search(startingShape, eps);
	}
		
	
	// The method that searches for a minimum until convergence:
	public int search(vector[] startingShape, double eps) {
		verticies = startingShape;
		calculateValues();
		updateHiLo();
		int steps = 0;
		while(steps < 10000 && size > eps) {
			Error.Write($"Step nr: {steps}\n");
			steps++;
			// Start by finding the reflection point:
			vector p_re = reflection();
			double f_re = phi(p_re);
			if(f_re < values[lo]) {
				// Try expansion:
				vector p_ex = expansion();
				double f_ex = phi(p_re);
				if(f_ex < f_re) {
					// Accept the expansion:
					// The expansion point is the new lowest point, but it
					// is the highest point that was moved:
					verticies[hi] = p_ex;
					values[hi] = f_ex;
					// Update which points are the highest and lowest:
					updateHiLo();
				} else {
					// If not the expansion, then accept the reflection:
					verticies[hi] = p_re;
					values[hi] = f_re;
					updateHiLo();
				}
			} else {
				if(f_re < values[hi]) {
					// Accept reflection:
					verticies[hi] = p_re;
					values[hi] = f_re;
					updateHiLo();
				} else {
					// Try contraction:
					vector p_co = contraction();
					double f_co = phi(p_co);
					if(f_co < values[hi]) {
						// Accept the contraction:
						verticies[hi] = p_co;
						values[hi] = f_co;
						updateHiLo();
				
					} else {
						// Do Reduction;
						reduction();
						// Now I have to recalculate all the values:
						calculateValues();
						// update the high and lowpoints:
						updateHiLo();
					}
				}
			}
		}
		return steps;
		
	}

	// NEXT GROUP: Reflection, expansion, contraction and reduction.
	vector reflection() {
		return centroid + (centroid - verticies[hi]);
	}

	vector expansion() {
		return centroid + 2*(centroid - verticies[hi]);
	}
	
	vector contraction() {
		return centroid + 1.0/2 * (verticies[hi] - centroid);
	}

	void reduction() {
		for(int i = 0; i < n+1; i++) {
			if(i != lo) verticies[i] = 1.0/2 * (verticies[i] + verticies[lo]);
		}
	}

}


