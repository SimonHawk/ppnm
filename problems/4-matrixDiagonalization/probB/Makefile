all: out.B.txt B1.nDependence.svg B3.comparison.svg 
	cat out.B.txt

B3.comparison.svg: plotB3.gpi out.dataB3.txt
	gnuplot $<

B1.nDependence.svg: plotB1.gpi out.dataB1.txt
	gnuplot $<

out.B.txt out.dataB1.txt out.dataB3.txt: mainB.exe
	mono $< 1> out.B.txt

mainB.exe: mainB.cs math.dll
	mcs -reference:math.dll -out:$@ $<

EXT_LIB = ../../../lib
MATLIB = $(EXT_LIB)/matlib
SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/complex/cmath.cs
SRC += $(EXT_LIB)/leastSquares/leastSquares.cs
SRC += $(EXT_LIB)/qrDecomp/qr_abstract.cs
SRC += $(EXT_LIB)/qrDecomp/qr_gs.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/jacobiCyclic.cs
SRC += $(LOCAL_LIB)/jacobiValues.cs
SRC += $(LOCAL_LIB)/matrixHelp.cs
SRC += $(LOCAL_LIB)/hamilton.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean: 
	rm -f main*.exe *.dll out.*.txt *.svg
