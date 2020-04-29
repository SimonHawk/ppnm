all: out.logB.txt B.NDependence.svg

B.NDependence.svg: plotB.gpi out.dataB.txt
	gnuplot $< 

out.logB.txt out.dataB.txt: mainB.exe
	mono $< 1> out.logB.txt

mainB.exe: mainB.cs math.dll
	mcs -reference:math.dll -out:$@ $<

LIB = ../../../lib
MATLIB = $(LIB)/matlib
SRC := $(MATLIB)/matrix/matrix.cs
SRC += $(MATLIB)/matrix/vector.cs
LOCAL_LIB = ../lib
SRC += $(LOCAL_LIB)/mcIntegrator.cs

math.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.dll *.exe out.*.txt  
