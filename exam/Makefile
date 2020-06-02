all:out.demo.txt

out.demo.txt: demo/Makefile
	cd demo/ && make
	cp demo/$@ $@

.PHONY:clean
clean: 
	cd demo/ && make clean
	rm -f out.*.txt
