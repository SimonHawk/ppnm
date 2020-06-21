all: out.logC.txt C.ODE.svg
	cat out.logC.txt

C.ODE.svg: plotC.gpi out.dataC.txt
	gnuplot $<

out.logC.txt out.dataC.txt: mainC.exe
	mono $< 1> out.logC.txt

mainC.exe: mainC.cs math.dll
	mcs -out:$@ -reference:math.dll $<

LIB = ../../../lib
MATLIB = $(LIB)/matlib
SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/complex/cmath.cs
SRC += $(LIB)/minimization/qnewton.cs
SRC += $(LIB)/minimization/simplex.cs
SRC += $(LIB)/math/math.cs
SRC += $(LIB)/integration/integrator.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/ann.cs
SRC += $(LOCAL_LIB)/annGaussian.cs
SRC += $(LOCAL_LIB)/annODE.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f out.*.txt *.dll *.svg
