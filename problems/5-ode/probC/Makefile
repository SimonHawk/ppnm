all: out.logC.txt C-Figure8.svg
	cat out.logC.txt

C-Figure8.svg: plotC.gpi
	gnuplot $<

out.logC.txt out.probC.txt: mainC.exe
	mono $< 1> out.logC.txt

mainC.exe: mainC.cs math.dll
	mcs -reference:math.dll -out:$@ $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/matrix/matrix.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/rkode.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.exe out.*.txt *.svg math.dll
