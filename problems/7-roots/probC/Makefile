all: out.logC.txt C.rMaxDependence.svg
	cat out.logC.txt

C.rMaxDependence.svg: plotC.gpi out.probC.txt
	gnuplot $<

out.logC.txt out.probC.txt: mainC.exe
	mono $< 1> out.logC.txt

mainC.exe: mainC.cs math.dll
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

