ExA3.svg: exA3.gpi out.exA3.txt out.cgamma.txt
	gnuplot $<

out.cgamma.txt: gammaTest.c
	tcc -run $< > $@

out.exA3.txt: main-exA3.exe
	mono $< > $@

LIBDIR = ../lib

main-exA3.exe: main-exA3.cs math.dll $(LIBDIR)/cmath.dll
	mcs -reference:math.dll,$(LIBDIR)/cmath.dll -out:$@ $<

math.dll: ../math.cs $(LIBDIR)/cmath.dll
	mcs -reference:$(LIBDIR)/cmath.dll -target:library -out:$@ $<

$(LIBDIR)/cmath.dll: $(LIBDIR)/cmath.cs $(LIBDIR)/complex.cs
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f $(LIBDIR)/*.dll  math.dll *.exe out.*.txt *.dat *.svg

