all: out.logC.txt

out.logC.txt out.probC.txt: mainC.exe
	mono $< 1> out.logC.txt

mainC.exe: mainC.cs math.dll
	mcs -reference:math.dll -out:$@ $<

LIB = ../../../lib
MATLIB = $(LIB)/matlib
SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/complex/cmath.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/simplex.cs

math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.dll *.exe out.*.txt *.svg 

