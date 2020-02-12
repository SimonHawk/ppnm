all: out.txt
	cat out.txt

out.txt: main.exe
	mono main.exe > out.txt

main.exe: main.cs approx.dll
	mcs -out:$@ -reference:approx.dll main.cs

approx.dll: approx.cs cmath.dll
	mcs -out:$@ -target:library -reference:cmath.dll approx.cs

cmath.dll: cmath.cs complex.cs
	mcs -out:$@ -target:library $^

.PHONY: clean
clean:
	rm -f out.txt main.exe approx.dll cmath.dll
