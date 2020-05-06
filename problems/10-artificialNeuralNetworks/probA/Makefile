all: out.logA.txt
	cat out.logA.txt

out.logA.txt: mainA.exe
	mono $< 1> $@

mainA.exe: mainA.cs math.dll
	mcs -out:$@ -reference:math.dll $<

LIB = ../../../lib
MATLIB = $(LIB)/matlib
SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/complex/cmath.cs
SRC += $(LIB)/minimization/qnewton.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/ann.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f out.*.txt *.dll
