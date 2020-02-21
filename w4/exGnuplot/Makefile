all: out.txt
	cat out.txt

out.txt outfile.dat: main.exe
	mono main.exe > out.txt

main.exe: main.cs cmath.dll
	mcs -out:$@ -reference:cmath.dll $<

cmath.dll: lib/cmath.cs lib/complex.cs
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f cmath.dll main.exe out.txt *.dat
