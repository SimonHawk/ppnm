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
        Shows that my implemented Lanczos algorithm produces the intended 
        V and T matricies for a random symmetric matrix.

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

    convergence_size.svg:
       Compares how fast the highest eigenvalue converges in m for different
       matrix sizes, n. Surprisingly, it seems that the convergence actually
       is just a little bit faster for larger m. It seems that for m = 10, 
       the the minimum deviation of highest eigenvalue is found in all 
       cases regardless of matrix size.
       However, it also seem that for larger matricies, the deviation from
       the "exact" result found by direct Jacobi-rotations gets larger. 
       This might be due to the larger numerical errors found when treating
       bigger collections of numbers with the larger matricies. 

    runtime.svg:
       Compares the runtime to calculate the highest eigenvalue of an 
       random symmetric matrix of size n by either:
           1: Using the my single value Jacobi method from Problem 4
           2: Using the Lanczos algorithm for m = 10 itterations and then
              calculating all the eigenvalues of the resulting 10x10 matrix
              by the full jacobi algorithm.
       In principle, this could be sped up by only calculating the highest
       eigenvalue of the Lanczos reduced matrix, but as the matrix is always
       10x10 this shouldn't matter much.
       Conclusion: Using the Lanczos algorithm first provides a massive
       speedup over the direct single value Jacobi method. However, my
       single value Jacobi never did work that well...
       Furthermore, as shown with comparison_size.svg, the Lanczos approach
       probably looses out on accuracy at the higher n. However, I have no
       practical way of testing this, as calculating the highest eigenvalue
       for n > 1000 using any other method I have access to would take a 
       long time.



The implementation of the numerical method can be found in the /exam/lib/ 
folder.

The demonstration code that outputs the demonstration files can be found in 
the /exam/demo/ folder.


--- FORMATTING ---
I have written the code to fit in a terminal that is 80 characters wide.
