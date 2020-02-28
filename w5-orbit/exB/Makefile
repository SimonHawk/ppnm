all:ExBi.svg ExBii.svg ExBiii.svg


ExBiii.svg: exBiii.gpi outBiii.txt
	gnuplot $<

outBiii.txt: mainB.exe Makefile
	mono $< xa=0 xb=62.8 eps=0.01 y0=1 y1=-0.5 > $@

ExBii.svg: exBii.gpi outBii.txt
	gnuplot $<

outBii.txt: mainB.exe Makefile
	mono $< y1=-0.5 > $@

ExBi.svg: exBi.gpi outBi.txt
	gnuplot $<

outBi.txt: mainB.exe Makefile
	mono $< > $@

mainB.exe: mainB.cs matlib.dll
	mcs -reference:matlib.dll -out:$@ $<

MATLIB = ../../lib/matlib
matlib.dll: $(MATLIB)/matrix/vector.cs $(MATLIB)/ode/ode.cs
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.txt *.dll *.exe *.svg
	
test:
	echo $$0
