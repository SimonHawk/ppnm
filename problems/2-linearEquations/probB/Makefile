all: out.B.txt
	cat out.B.txt

out.B.txt: mainB.exe
	mono $< 1> $@

mainB.exe: mainB.cs math.dll
	mcs -out:$@ -reference:math.dll $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/complex/cmath.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/qr_gs.cs
SRC += $(LOCAL_LIB)/matrixHelp.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean: 
	rm -f out.*.txt *.dll *.exe
