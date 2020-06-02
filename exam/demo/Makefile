all: out.demo.txt

out.eigen.txt: eigen.exe
	mono $< 1> $@

eigen.exe: eigen.cs math.dll
	mcs -reference:math.dll -out:$@ $<

out.demo.txt: demo.exe
	mono $< 1> $@

demo.exe: demo.cs math.dll
	mcs -reference:math.dll -out:$@ $<

LIB = ../../lib
LOCAL_LIB = ../lib
MATLIB = $(LIB)/matlib

SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
SRC += $(LOCAL_LIB)/matrixHelp.cs
SRC += $(LOCAL_LIB)/lanczos.cs

math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f out.*.txt *.dll
