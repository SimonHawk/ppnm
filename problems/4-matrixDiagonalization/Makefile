all: probA probB probC

probC: C.comparison.svg out.C.txt

probB: B1.nDependence.svg B3.comparison.svg out.B.txt

probA: out.A.txt

C.comparison.svg out.C.txt:
	cd probC/ && make
	cp probC/*.svg ./
	cp probC/out.C.txt ./

B1.nDependence.svg B3.comparison.svg out.B.txt:
	cd probB/ && make
	cp probB/*.svg ./
	cp probB/out.B.txt ./

out.A.txt A.eigenfunctions.svg: probA/out.A.txt probA/A.eigenfunctions.svg
	cd probA/ && cp out.A.txt ../ && cp A.eigenfunctions.svg ../

probA/out.A.txt probA/A.eigenfunctions.svg: probA/Makefile
	cd probA/ && make

.PHONY:
clean:
	cd probA/ && make clean
	cd probB/ && make clean
	cd probC/ && make clean
	rm -f out.*.txt *.svg
