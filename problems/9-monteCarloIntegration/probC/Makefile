all: out.logC.txt C.comparison.svg C.sampledPoints.svg

C.sampledPoints.svg: plotC2.gpi out.dataC.txt
	gnuplot $< 

C.comparison.svg: plotC.gpi out.dataC.txt
	gnuplot $< 

out.logC.txt out.dataC.txt: mainC.exe
	mono $< 1> out.logC.txt

mainC.exe: mainC.cs math.dll
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
	rm -f *.dll *.exe out.*.txt *.svg *.png
