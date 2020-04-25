all: out.logA.txt out.logB.txt B.foundWavefunction.svg out.logC.txt C.rMaxDependence.svg

C.rMaxDependence.svg: probC/C.rMaxDependence.svg
	cp $< $@

out.logC.txt: probC/out.logC.txt
	cp $< $@

probC/out.logC.txt probC/C.rMaxDependence.svg: probC/Makefile
	cd probC/ && make

B.foundWavefunction.svg: probB/B.foundWavefunction.svg
	cp $< $@

out.logB.txt: probB/out.logB.txt
	cp $< $@

probB/out.logB.txt probB/B.foundWavefunction.svg: probB/Makefile
	cd probB/ && make

out.logA.txt: probA/out.logA.txt
	cp $< $@

probA/out.logA.txt: probA/Makefile
	cd probA/ && make

.PHONY:clean
clean:
	cd probA/ && make clean
	cd probB/ && make clean
	cd probC/ && make clean
	rm -f out.*.txt *.svg
