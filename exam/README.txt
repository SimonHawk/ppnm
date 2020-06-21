--- INTRO: ---
In this folder, you will find my solution to the PPNM exam project.

My student number is 201609122.
There are 22 project, and thereby, my assigned project is:
mod(22, 22) = 0

Number 0: Lanczos tridiagonalization algorithm.


--- STRUCTURE: ---
In this folder (/exam/) you will find output .txt and .svg files that 
demonstrate that my solution works as intended:

	out.demo.txt:   
        Shows that my implemented Lanczos algorithm produces
	    the intended V and T matricies for a random symmetric
	    matrix.

	out.eigen.txt:  
        Demonstrates how the Lanczos algorithm can be used to find correct 
        eigenvalues and eigenvectors of a random symmetric matrix.

	out.convergence.txt:  
        Intruduces the test of convergence of the eigenvalues and vectors 
        with the number of itterations which is shown in convergence.svg
	
	convergence.svg:      
        Shows how the eigen-values and -vectors converge for a number of 
        itterations going to the size of the matrix. 
        Please note that BOTH for the highest and lowest eigenvalues 
        converge, but that the highest converge the fastest.

	convergence_lowest.svg:  
       Shows that the algorithm can be optimized for the convergence of the
       lowest eigenvalues instead of the higest. This was achived by
       changeing the application of the matrix (A*v1 = v2) to an inversion
       (by solving the system: A*v2 = v1 using QR decomposition).
                             
       And in comparison with convergence.svg: 
       Notice that the respective methods seem to make their higest/lowest 
       eigenvalues converge equally fast in m.
       However, the second lowest, third lowest and so on seem to converge
       faster for the "lowest" approach, than the second highest, third
       highest does for the "highest" approach.
       And on the other side, the highest eigenvalues for the "lowest"
       approach seem to converge slower than the lowest eigenvalues for
       the "highest" approach.
       

The implementation of the numerical method can be found in the /exam/lib/ 
folder.

The demonstration code that outputs the demonstration files can be found in 
the /exam/demo/ folder.


--- FORMATTING ---
I have written the code to fit in a terminal that is 80 characters wide.
