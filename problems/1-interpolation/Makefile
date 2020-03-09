all: plotA.svg 

plotA.svg: plotA.gpi out.A.txt testDataA.txt
	gnuplot $<

out.A.txt: mainA.exe
	mono $< 1> $@

mainA.exe: mainA.cs math.dll
	mcs -out:$@ -reference:math.dll $<

testDataA.txt: dataMaker.exe
	mono $< 1> $@

dataMaker.exe: dataMaker.cs math.dll
	mcs -out:$@ -reference:math.dll $<

MATLIB = ../../lib/matlib
SRC := $(MATLIB)/matrix/vector.cs
math.dll: $(SRC) search.cs linterp.cs dataMaker.cs
	mcs -target:library -out:$@ $^
