all: ms.pdf

ms.pdf: ms.tex plot-exp.tex
	pdflatex $<
	pdflatex $<

plot-exp.tex: plot.gpi out.exp.txt out-table.txt
	gnuplot $<

out.exp.txt: main.exe
	mono $< > $@

main.exe: main.cs exp.cs matlib.dll
	mcs $(word 1, $^) $(word 2, $^) -reference:$(word 3, $^)

LIBDIR = ../../lib/matlib
SRC := $(LIBDIR)/matrix/vector.cs
SRC += $(LIBDIR)/ode/ode.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	rm -f *.log *.out *.aux *.pdf *.dll *.exe *.dvi *.eps plot-exp.tex

test:
	echo $(SRC)
