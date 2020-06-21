all: out.demo.txt out.eigen.txt data.convergence.txt convergence.svg convergence_lowest.svg 

convergence_lowest.svg: convergence_lowest.gpi data.convergence_lowest.txt
	gnuplot $<

data.convergence_lowest.txt: convergence_lowest.exe
	mono $< 

convergence_lowest.exe: convergence_lowest.cs
	mcs -reference:math.dll -out:$@ $<

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
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/complex/cmath.cs
SRC += $(LIB)/diagonalization/jacobiCyclic.cs
SRC += $(LIB)/diagonalization/jacobiValues.cs
SRC += $(LIB)/diagonalization/jacobiClassic.cs
SRC += $(LIB)/qrDecomp/qr_abstract.cs
SRC += $(LIB)/qrDecomp/qr_givens.cs
SRC += $(LOCAL_LIB)/matrixHelp.cs
SRC += $(LOCAL_LIB)/lanczos.cs

math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f out.*.txt data.*.txt *.dll *.exe *.svg
