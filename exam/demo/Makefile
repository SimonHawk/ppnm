all: out.demo.txt out.eigen.txt data.convergence.txt convergence.svg

convergence.svg: convergence.gpi data.convergence.txt
	gnuplot $<

out.convergence.txt data.convergence.txt: convergence.exe
		mono $< 1> out.convergence.txt

convergence.exe: convergence.cs
	mcs -reference:math.dll -out:$@ $<

out.eigen.txt: eigen.exe
	mono $< 1> $@

eigen.exe: eigen.cs math.dll
	mcs -reference:math.dll -out:$@ $<

out.demo.txt: demo.exe
	mono $< 1> $@

demo.exe: demo.cs math.dll
	mcs -reference:math.dll -out:$@ $<

LIB = ../../lib
LOCAL_LIB = ../lib
MATLIB = $(LIB)/matlib

SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(LIB)/diagonalization/jacobiCyclic.cs
SRC += $(LIB)/diagonalization/jacobiValues.cs
SRC += $(LIB)/diagonalization/jacobiClassic.cs
SRC += $(LOCAL_LIB)/matrixHelp.cs
SRC += $(LOCAL_LIB)/lanczos.cs

math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f out.*.txt data.*.txt *.dll *.exe *.svg
