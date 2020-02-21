all: ExA2.svg ExA3.svg ExB.svg

ExB.svg: exB.gpi out.exB.txt
	gnuplot $<

out.exB.txt: main-exB.exe
	mono $< > $@

main-exB.exe: main-exB.cs math.dll
	mcs -reference:math.dll -out:$@ $<

ExA3.svg: exA3.gpi out.exA3.txt out.cgamma.txt
	gnuplot $<

out.cgamma.txt: gammaTest.c
	tcc -run $< > $@

out.exA3.txt: main-exA3.exe
	mono $< > $@

main-exA3.exe: main-exA3.cs math.dll
	mcs -reference:math.dll -out:$@ $<

ExA2.svg: exA2.gpi out.exA2.txt
	gnuplot $<		

out.exA2.txt: main-exA2.exe
	mono $< > $@

main-exA2.exe: main-exA2.cs math.dll
	mcs -reference:math.dll -out:$@  $<

math.dll: math.cs
	mcs -target:library -out:$@ $<

cmath.dll: lib/cmath.cs lib/complex.cs
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f math.dll *.exe out.*.txt *.dat *.svg
