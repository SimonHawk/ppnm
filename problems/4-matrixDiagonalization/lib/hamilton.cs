public class hamilton {
	public static matrix boxHamilton(int N) {
		matrix H = new matrix(N, N);
		for(int i = 0; i < N; i++) {
			H[i,i] = -2.0;
		}
		for(int i = 0; i < N-1; i++) {
			H[i, i+1] = 1.0;
			H[i+1, i] = 1.0;
		}
		H = -(N+1)*(N+1)*H;
		return H;
	}
}
