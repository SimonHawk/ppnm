all: ExC.svg

ExC.svg: exC.gpi out.exC.txt
	gnuplot $<

out.exC.txt out.reCoords.txt out.imCoords.txt: main-exC.exe
	mono $< > $@

main-exC.exe: main-exC.cs math.dll cmath.dll
	mcs -reference:math.dll,cmath.dll -out:$@ $< 

math.dll: ../math.cs cmath.dll
	mcs -reference:cmath.dll -target:library -out:$@ $<

cmath.dll: ../lib/cmath.cs ../lib/complex.cs
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f math.dll *.exe out.*.txt *.dat *.svg
