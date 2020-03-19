PlotA.svg: probA/PlotA.svg
	cp $< ./

probA/PlotA.svg: probA/Makefile
	cd probA && make PlotA.svg

.PHONY:clean
clean:
	cd probA && make clean
	rm -f Plot*.svg
