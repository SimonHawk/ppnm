all: out.logB.txt
	cat out.logB.txt

out.logB.txt: mainB.exe
	mono $< 1> out.logB.txt

mainB.exe: mainB.cs math.dll
	mcs -out:$@ -reference:math.dll $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/vector.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/integrator.cs

math.dll: $(SRC)
	mcs -target:library -out:$@ $^

