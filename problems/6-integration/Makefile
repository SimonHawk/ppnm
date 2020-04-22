all: out.logB.txt B.PiAccuracy.svg out.logA.txt 

B.PiAccuracy.svg: probB/B.PiAccuracy.svg
	cp $< $@

out.logB.txt: probB/out.logB.txt
	cp $< $@

probB/out.logB.txt probB/B.PiAccuracy.svg: probB/Makefile
	cd probB/ && make

out.logA.txt: probA/out.logA.txt
	cp $< $@

probA/out.logA.txt: probA/Makefile
	cd probA/ && make

.PHONY: clean
clean:
	cd probA/ && make clean
	cd probB/ && make clean
	rm -f out.*.txt *.svg
