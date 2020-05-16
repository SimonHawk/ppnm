all: out.logB.txt SIRModel.svg
	cat out.logB.txt

SIRModel.svg: plotB.gpi
	gnuplot $<

out.logB.txt out.probB.txt: mainB.exe
	mono $< 1> out.logB.txt

mainB.exe: mainB.cs math.dll
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
