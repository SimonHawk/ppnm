plot.svg: plot.gpi out.txt
	gnuplot $<

out.txt: main.exe Makefile
	mono $< a=0 y0=0 y1=1 b=50 1> out.txt 2> log

main.exe: main.cs matlib.dll
	mcs -reference:matlib.dll $<

MATLIB = ../../lib/matlib
matlib.dll: $(MATLIB)/matrix/vector.cs $(MATLIB)/ode/ode.cs
	mcs -target:library -out:$@ $^ 
