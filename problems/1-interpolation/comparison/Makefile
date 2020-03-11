all: plotComparison.svg 

plotComparison.svg: plotComparison.gpi out.comparison.txt out.points.txt
	gnuplot $<

out.comparison.txt out.points.txt: comparison.exe
	mono $< 1> out.comparison.txt 2> out.points.txt

comparison.exe: comparison.cs math.dll
	mcs -out:$@ -reference:math.dll $<

LOCAL_LIB = ../lib
MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/vector.cs
SRC += $(LOCAL_LIB)/search.cs
SRC += $(LOCAL_LIB)/linterp.cs
SRC += $(LOCAL_LIB)/qspline.cs
SRC += $(LOCAL_LIB)/cspline.cs
SRC += $(LOCAL_LIB)/dataMaker.cs
math.dll: $(SRC)	
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.exe out.*.txt *.svg *.dll 
