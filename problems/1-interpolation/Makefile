all: PlotA.svg PlotB.svg PlotC.svg

PlotA.svg: probA/PlotA.svg
	cp probA/PlotA.svg ./

probA/PlotA.svg: probA/Makefile
	cd probA/ && make

PlotB.svg: probB/PlotB.svg
	cp probB/PlotB.svg ./

probB/PlotB.svg: probB/Makefile
	cd probB/ && make

PlotC.svg: probC/PlotC.svg
	cp probC/PlotC.svg ./

probC/PlotC.svg: probC/Makefile
	cd probC/ && make

.PHONY:clean
clean:
	cd probA/ && make clean
	cd probB/ && make clean
	cd probC/ && make clean
	rm -f PlotA.svg PlotB.svg PlotC.svg	
