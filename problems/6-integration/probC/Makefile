all: out.logC.txt
	cat out.logC.txt

out.logC.txt: mainC.exe
	mono $< 1> out.logC.txt

mainC.exe: mainC.cs math.dll
	mcs -out:$@ -reference:math.dll $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/integration/quado8.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/integrator.cs

math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.dll *.exe out.*.txt
