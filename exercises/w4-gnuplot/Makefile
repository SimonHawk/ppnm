all: ExA2.svg ExA3.svg ExB.svg ExC.svg

ExA2.svg: exA2/Makefile
	cd exA2/ && make
	cp exA2/ExA2.svg ./

ExA3.svg: exA3/Makefile
	cd exA3/ && make
	cp exA3/ExA3.svg ./

ExB.svg: exB/Makefile
	cd exB/ && make
	cp exB/ExB.svg ./

ExC.svg: exC/Makefile
	cd exC/ && make
	cp exC/ExC.png ./

.PHONY:clean
clean:
	rm -f Ex*.svg
	cd exA2/ && make clean
	cd exA3/ && make clean
	cd exB/ && make clean
	cd exC/ && make clean
