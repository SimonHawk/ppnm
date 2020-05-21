all: out.C.txt C.comparison.svg
	cat out.C.txt

C.comparison.svg: plotC.gpi out.dataC.txt
	gnuplot $<

out.C.txt out.dataC.txt: mainC.exe
	mono $< 1> out.C.txt

mainC.exe: mainC.cs math.dll
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
SRC += $(LOCAL_LIB)/jacobiClassic.cs
SRC += $(LOCAL_LIB)/matrixHelp.cs
SRC += $(LOCAL_LIB)/hamilton.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean: 
	rm -f main*.exe *.dll out.*.txt *.svg
