using static System.Math;
using static System.Console;

public static class search {
    // Returns the index of the nearest smaller to z in the vector v.
    // That is: returns idx, z is in the range [v[idx], v[idx+1]]
    public static int binary_search(vector v, double z) {
        int L = 0;
        int R = v.size-1;
        int m;
        while(L<=R) {
            m = (int) Floor((L+R)/2.0);
            if(z > v[m]) {
               L = m;
            } else {
                R = m;
            }
            if(R-L == 1) {
                return L;
            }
        }
        // Return -1 (false index) if the algorith failed
        return -1;
    }
}

