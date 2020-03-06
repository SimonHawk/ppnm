all: plot-bessel.svg
 
ms.pdf: ms.tex plot-SandC.tex plot-euler.tex references.bib
	pdflatex $<
	bibtex ms.aux 
	pdflatex $<
	pdflatex $<

plot-euler.tex: plot.euler.gpi out.funcs.txt
	gnuplot $<

plot-SandC.tex: plot.SandC.gpi out.funcs.txt
	gnuplot $<

plot-bessel.svg: plot.bessel.gpi out.bessel.txt
	gnuplot $<
	
out.bessel.txt: mainBessel.exe
	mono $< 1> out.bessel.txt

mainBessel.exe: mainBessel.cs math.dll
	mcs $< -reference:math.dll -out:$@

out.funcs.txt: main.exe
	mono $< 1> out.funcs.txt

main.exe: main.cs math.dll
	mcs $< -reference:math.dll -out:$@ 

MATLIB = ../lib/matlib
SRC := $(MATLIB)/integration/quado8.cs
SRC += $(MATLIB)/ode/ode.cs
SRC += $(MATLIB)/complex/cmath.cs
SRC += $(MATLIB)/complex/complex.cs
SRC += $(MATLIB)/matrix/vector.cs
math.dll: $(SRC) functions.cs besselFun.cs
	mcs $^ -target:library -out:$@

.PHONY:clean
clean:
	rm -f *.exe out.*.txt *.dll *.pdf plot-*.tex *.aux *.bbl *.blg
