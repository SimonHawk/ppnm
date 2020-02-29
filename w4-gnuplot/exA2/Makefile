ExA2.svg: exA2.gpi out.exA2.txt
	gnuplot $<      

out.exA2.txt: main-exA2.exe
	mono $< > $@

main-exA2.exe: main-exA2.cs math.dll
	mcs -reference:math.dll -out:$@  $<

LIBDIR = ../lib
math.dll: ../math.cs $(LIBDIR)/cmath.dll
	mcs -reference:$(LIBDIR)/cmath.dll -target:library -out:$@ $<

$(LIBDIR)/cmath.dll: $(LIBDIR)/cmath.cs $(LIBDIR)/complex.cs
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f lib/*.dll  math.dll *.exe out.*.txt *.dat *.svg
