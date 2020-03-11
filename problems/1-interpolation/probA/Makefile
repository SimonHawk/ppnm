all: plotA.svg 

plotA.svg: plotA.gpi out.A.txt out.testDataA.txt
	gnuplot $<

out.A.txt: mainA.exe
	mono $< 1> $@

mainA.exe: mainA.cs math.dll out.testDataA.txt
	mcs -out:$@ -reference:math.dll $<

out.testDataA.txt: dataMaker.exe
	mono $< 1> $@

LOCAL_LIB = ../lib
dataMaker.exe: $(LOCAL_LIB)/dataMaker.cs math.dll
	mcs -out:$@ -reference:math.dll $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/vector.cs
SRC += $(LOCAL_LIB)/search.cs
SRC += $(LOCAL_LIB)/linterp.cs
SRC += $(LOCAL_LIB)/dataMaker.cs
math.dll: $(SRC)	
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.exe out.*.txt *.svg *.dll 
