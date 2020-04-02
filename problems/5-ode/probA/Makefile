all: out.logA.txt PlotA.svg
	cat out.logA.txt

PlotA.svg: plotA.gpi out.probA.txt
	gnuplot $<

out.logA.txt out.probA.txt: mainA.exe
	mono $< 1> out.logA.txt

mainA.exe: mainA.cs math.dll
	mcs -reference:math.dll -out:$@ $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/vector.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/rkode.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.exe out.*.txt *.svg math.dll
