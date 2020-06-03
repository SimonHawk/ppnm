all:plots outfiles

plots: demo/Makefile
	cd demo/ && make
	cp demo/*.svg ./

outfiles: demo/Makefile
	cd demo/ && make
	cp demo/out.*.txt ./

.PHONY:clean
clean: 
	cd demo/ && make clean
	rm -f out.*.txt *.svg
