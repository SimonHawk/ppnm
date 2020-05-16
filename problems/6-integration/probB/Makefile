all: out.logB.txt B.PiAccuracy.svg

B.PiAccuracy.svg: plotB.gpi out.PIAccuracy.txt
	gnuplot $<

out.logB.txt out.PIAccuracy.txt: mainB.exe
	mono $< 1> out.logB.txt

mainB.exe: mainB.cs math.dll
	mcs -out:$@ -reference:math.dll $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/integration/quado8.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/integrator.cs

math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.dll out.*.txt *.exe *.svg
