all: ExA2.svg ExA3.svg ExB.svg ExC.svg

ExC.svg: exC.gpi out.exC.txt
	gnuplot $<

out.exC.txt: main-exC.exe
	mono $< > $@

main-exC.exe: main-exC.cs math.dll lib/cmath.dll
	mcs -reference:math.dll,lib/cmath.dll -out:$@ $< 

ExB.svg: exB/exB.gpi exB/out.exB.txt
	gnuplot $<

exB/out.exB.txt: exB/main-exB.exe
	mono $< > $@

exB/main-exB.exe: exB/main-exB.cs math.dll lib/cmath.dll
	mcs -reference:math.dll,lib/cmath.dll -out:$@ $<

ExA3.svg: exA3.gpi out.exA3.txt out.cgamma.txt
	gnuplot $<

out.cgamma.txt: gammaTest.c
	tcc -run $< > $@

out.exA3.txt: main-exA3.exe
	mono $< > $@

main-exA3.exe: main-exA3.cs math.dll lib/cmath.dll 
	mcs -reference:math.dll,lib/cmath.dll -out:$@ $<

ExA2.svg: exA2.gpi out.exA2.txt
	gnuplot $<		

out.exA2.txt: main-exA2.exe
	mono $< > $@

main-exA2.exe: main-exA2.cs math.dll
	mcs -reference:math.dll -out:$@  $<

math.dll: math.cs lib/cmath.dll
	mcs -reference:lib/cmath.dll -target:library -out:$@ $<

lib/cmath.dll: lib/cmath.cs lib/complex.cs
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f lib/*.dll  math.dll *.exe out.*.txt *.dat *.svg
