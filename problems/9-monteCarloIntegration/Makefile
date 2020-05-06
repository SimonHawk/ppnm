all: probA probB probC

probC: out.logC.txt C.comparison.svg C.sampledPoints.png

out.logC.txt C.comparison.svg C.sampledPoints.png: probC/out.logC.txt probC/C.comparison.svg probC/C.sampledPoints.png
	cp $^ ./

probC/out.logC.txt probC/C.comparison.svg probC/C.sampledPoints.png: probC/Makefile
	cd probC/ && make

probB: out.logB.txt B.sigmaNDependence.svg

out.logB.txt B.sigmaNDependence.svg: probB/out.logB.txt probB/B.sigmaNDependence.svg
	cp $^ ./

probB/out.logB.txt probB/B.sigmaNDependence.svg: probB/Makefile
	cd probB/ && make

probA: out.logA.txt

out.logA.txt: probA/out.logA.txt
	cp $< $@

probA/out.logA.txt: probA/Makefile
	cd probA/ && make

.PHONY:clean
clean:
	cd probA/ && make clean
	cd probB/ && make clean
	cd probC/ && make clean
	rm -f out.*.txt *.png *.svg
