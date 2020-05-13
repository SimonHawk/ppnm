all: out.logB.txt B.interpolation.svg
	cat out.logB.txt

B.interpolation.svg: plotB.gpi out.dataB.txt
	gnuplot $<

out.logB.txt out.dataB.txt: mainB.exe
	mono $< 1> $@

mainB.exe: mainB.cs math.dll
	mcs -out:$@ -reference:math.dll $<

LIB = ../../../lib
MATLIB = $(LIB)/matlib
SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/complex/cmath.cs
SRC += $(LIB)/minimization/qnewton.cs
SRC += $(LIB)/math/math.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/ann.cs
SRC += $(LOCAL_LIB)/annGaussian.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f out.*.txt *.dll *.svg
