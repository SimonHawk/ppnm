all: out.A.txt PlotA.svg
	cat out.A.txt

PlotA.svg: plotA.gpi out.dataA.txt
	gnuplot $<

out.A.txt out.dataA.txt: mainA.exe
	mono $< 1> $@

mainA.exe: mainA.cs math.dll
	mcs -reference:math.dll -out:$@ $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/jacobi.cs
SRC += $(LOCAL_LIB)/matrixHelp.cs
SRC += $(LOCAL_LIB)/hamilton.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^
