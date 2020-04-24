all: out.logA.txt
	cat out.logA.txt

out.logA.txt: mainA.exe
	mono $< 1> out.logA.txt

mainA.exe: mainA.cs math.dll
	mcs -reference:math.dll -out:$@ $<

LIB = ../../../lib
SRC := $(LIB)/qrDecomp/qr_abstract.cs
SRC += $(LIB)/qrDecomp/qr_givens.cs
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

