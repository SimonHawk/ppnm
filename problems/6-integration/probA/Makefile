all: out.logA.txt

out.logA.txt: mainA.exe
	mono $< 1> out.logA.txt

mainA.exe: mainA.cs math.dll
	mcs -out:$@ -reference:math.dll $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/matrix/matrix.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/integrator.cs

math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.dll *.exe out.*.txt
