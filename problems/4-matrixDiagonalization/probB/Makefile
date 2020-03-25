all: out.B.txt PlotB.svg
	cat out.B.txt

PlotB.svg: plotB.gpi out.dataBi.txt
	gnuplot $<

out.B.txt out.dataBi.txt: mainB.exe
	mono $< 1> $@

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
SRC += $(LOCAL_LIB)/jacobi.cs
SRC += $(LOCAL_LIB)/matrixHelp.cs
SRC += $(LOCAL_LIB)/hamilton.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean: 
	rm -f main*.exe *.dll out.*.txt Plot*.svg
