all: plotB.svg 

plotB.svg: plotB.gpi out.B.txt out.testData.txt
	gnuplot $<

out.B.txt: mainB.exe out.testData.txt
	mono $< out.testData.txt 1> $@

mainB.exe: mainB.cs math.dll out.testData.txt
	mcs -out:$@ -reference:math.dll $<

out.testData.txt: dataMaker.exe
	mono $< 1> $@

LOCAL_LIB = ../lib
dataMaker.exe: $(LOCAL_LIB)/dataMaker.cs math.dll
	mcs -out:$@ -reference:math.dll $<	

testQSpline.exe: testQSpline.cs math.dll
	mcs -out:$@ -reference:math.dll $<

MATLIB = ../../../lib/matlib
SRC := $(MATLIB)/matrix/vector.cs
SRC += $(LOCAL_LIB)/search.cs
SRC += $(LOCAL_LIB)/qspline.cs
SRC += $(LOCAL_LIB)/dataMaker.cs
math.dll: $(SRC)	
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.exe out.*.txt *.svg *.dll 
