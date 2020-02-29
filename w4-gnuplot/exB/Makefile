ExB.svg: exB.gpi out.exB.txt
	gnuplot $<

out.exB.txt: main-exB.exe
	mono $< > $@

LIBDIR = ../lib

main-exB.exe: main-exB.cs math.dll $(LIBDIR)/cmath.dll
	mcs -reference:math.dll,$(LIBDIR)/cmath.dll -out:$@ $<

math.dll: ../math.cs $(LIBDIR)/cmath.dll
	mcs -reference:$(LIBDIR)/cmath.dll -target:library -out:$@ $<

$(LIBDIR)/cmath.dll: $(LIBDIR)/cmath.cs $(LIBDIR)/complex.cs
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f $(LIBDIR)/*.dll  math.dll *.exe out.*.txt *.dat *.svg

