all: B.higgsFit.svg out.logB.txt

B.higgsFit.svg: plotB.gpi higgsData.txt
	gnuplot $<

out.logB.txt: mainB.exe
	mono $< 1> $@

mainB.exe: mainB.cs math.dll
	mcs -reference:math.dll -out:$@ $<

LIB = ../../../lib
MATLIB = $(LIB)/matlib
SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/complex/cmath.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/qnewton.cs
SRC += $(LOCAL_LIB)/dataFitter.cs

math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.dll *.exe out.*.txt *.svg 

