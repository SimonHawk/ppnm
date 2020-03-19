all: out.B.txt PlotB.svg
	cat out.B.txt

PlotB.svg: plotB.gpi out.bestFitB.txt
	gnuplot $<

out.B.txt out.bestFitB.txt: mainB.exe
	mono $< 1> out.B.txt

mainB.exe: mainB.cs math.dll
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
