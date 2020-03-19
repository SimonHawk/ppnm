all: out.A.txt PlotA.svg
	cat out.A.txt

PlotA.svg: plotA.gpi out.bestFit.txt
	gnuplot $<

out.A.txt out.bestFit.txt: mainA.exe
	mono $< 1> $@

mainA.exe: mainA.cs math.dll
	mcs -out:$@ -reference:math.dll $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/complex/cmath.cs
QR_LIB = ../../../lib/qrDecomp
SRC += $(QR_LIB)/qr_abstract.cs
SRC += $(QR_LIB)/qr_gs.cs
SRC += $(QR_LIB)/matrixHelp.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/leastSquares.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean: 
	rm -f out.*.txt *.dll *.exe
