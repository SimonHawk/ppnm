#include<math.h>
#include<stdio.h>

int main() {
	for(double x = -3.5; x <= 3.5; x += 1) {
		printf("%g %g\n", x, tgamma(x));
	}
	return 0;
}
