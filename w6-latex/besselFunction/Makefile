msBessel.pdf: msBessel.tex
	pdflatex $<
	bibtex msBessel.aux
	pdflatex $<
	pdflatex $<

.PHONY:clean
clean:
	rm -f *.pdf *.aux *.bbl *.blg *.log

test:
	MONO_PATH=~/repos/ppnm && echo $$MONO_PATH
