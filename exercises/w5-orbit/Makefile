all: ExA.svg ExBi.svg ExBii.svg ExBiii.svg

ExBi.svg ExBii.svg ExBiii.svg: exB/Makefile
	cd exB/ && make
	cp exB/ExB*.svg ./

ExA.svg: exA/Makefile
	cd exA/ && make
	cp exA/ExA.svg ./

.PHONY:clean
clean:
	cd exA/ && make clean
	cd exB/ && make clean
	rm -f Ex*.svg

