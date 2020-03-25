
out.A.txt PlotA.svg: probA/out.A.txt probA/PlotA.svg
	cd probA/ && cp out.A.txt ../ && cp PlotA.svg ../

probA/out.A.txt probA/PlotA.svg: probA/Makefile
	cd probA/ && make

.PHONY:
clean:
	cd probA/ && make clean
	rm -f out.*.txt Plot*.svg
