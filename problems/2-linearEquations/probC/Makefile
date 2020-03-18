all: out.C.txt
	cat out.C.txt

out.C.txt: mainC.exe
	mono $< 1> $@

mainC.exe: mainC.cs math.dll
	mcs -out:$@ -reference:math.dll $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/complex/cmath.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/qr_givens.cs
SRC += $(LOCAL_LIB)/qr_abstract.cs
SRC += $(LOCAL_LIB)/qr_gs2.cs
SRC += $(LOCAL_LIB)/matrixHelp.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean: 
	rm -f out.*.txt *.dll *.exe
