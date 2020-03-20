all: PlotA.svg PlotC.svg out.A.txt out.B.txt

PlotC.svg out.B.txt: probBandC/PlotC.svg probBandC/out.B.txt
	cp $< ./
	cp probB/out.B.txt ./

probBandC/PlotC.svg probBandC/out.B.txt: probBandC/Makefile
	cd probBandC && make PlotC.svg

PlotA.svg out.A.txt: probA/PlotA.svg probA/out.A.txt
	cp $< ./
	cp probA/out.A.txt ./

probA/PlotA.svg probA/out.A.txt: probA/Makefile
	cd probA && make PlotA.svg

.PHONY:clean
clean:
	cd probA && make clean
	cd probBandC && make clean
	rm -f Plot*.svg out.*.txt
