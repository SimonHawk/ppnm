all: plotC.svg

plotC.svg: plotC.gpi out.C.txt out.testData.txt out.plotutilsSpline.txt
	gnuplot $<

out.plotutilsSpline.txt: out.testData.txt
	spline $< > $@

out.C.txt: mainC.exe out.testData.txt
	mono $< out.testData.txt 1> $@

mainC.exe: mainC.cs math.dll 
	mcs -out:$@ -reference:math.dll $< 
out.testData.txt: dataMaker.exe
	mono $< 1> $@

LOCAL_LIB = ../lib
dataMaker.exe: $(LOCAL_LIB)/dataMaker.cs math.dll
	mcs -out:$@ -reference:math.dll $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/matrix/matrix.cs
SRC += $(LOCAL_LIB)/search.cs
SRC += $(LOCAL_LIB)/cspline.cs
SRC += $(LOCAL_LIB)/dataMaker.cs
math.dll: $(SRC)	
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.exe out.*.txt *.svg *.dll 
