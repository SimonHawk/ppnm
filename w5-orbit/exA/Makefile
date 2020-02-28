all:ExA.svg

# --- Exercise A ---
ExA.svg: exA.gpi outA.txt
	gnuplot $<

outA.txt: mainA.exe
	mono $< > $@

mainA.exe: mainA.cs matlib.dll
	mcs -reference:matlib.dll -out:$@ $<

MATLIB = ../../lib/matlib
matlib.dll: $(MATLIB)/matrix/vector.cs $(MATLIB)/ode/ode.cs
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.txt *.dll *.exe
	
test:
	echo $$0
