ExC.svg: exC.gpi out.exC.txt
	gnuplot $<

out.exC.txt: main-exC.exe
	mono $< > $@

LIBDIR = ../lib

main-exC.exe: main-exC.cs math.dll cmath.dll
	mcs -reference:math.dll,cmath.dll -out:$@ $<

math.dll: ../math.cs $(LIBDIR)/cmath.dll
	mcs -reference:$(LIBDIR)/cmath.dll -target:library -out:$@ $<

cmath.dll: $(LIBDIR)/cmath.dll
	cp $< ./

$(LIBDIR)/cmath.dll: $(LIBDIR)/cmath.cs $(LIBDIR)/complex.cs
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f $(LIBDIR)/*.dll  math.dll *.exe out.*.txt *.dat *.svg

