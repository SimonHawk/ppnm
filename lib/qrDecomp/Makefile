MATLIB = ../matlib
SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/complex/cmath.cs
SRC += qr_abstract.cs
SRC += qr_gs.cs
SRC += qr_givens.cs
SRC += matrixHelp.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean: 
	rm -f out.*.txt *.dll *.exe
