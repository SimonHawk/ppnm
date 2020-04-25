all: out.logB.txt B.foundWavefunction.svg
	cat out.logB.txt

B.foundWavefunction.svg: plotB.gpi out.probB.txt
	gnuplot $<

out.logB.txt out.probB.txt: mainB.exe
	mono $< 1> out.logB.txt

mainB.exe: mainB.cs math.dll
	mcs -reference:math.dll -out:$@ $<

LIB = ../../../lib
SRC := $(LIB)/qrDecomp/qr_abstract.cs
SRC += $(LIB)/qrDecomp/qr_givens.cs
SRC += $(LIB)/ode/rkode.cs
MATLIB = $(LIB)/matlib
SRC += $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/complex/cmath.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/newton.cs

math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY: clean
clean:
	rm -f *.dll out.*.txt *.exe *.svg

