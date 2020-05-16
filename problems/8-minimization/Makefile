all: probA probB probC

probC:
	
probB: out.logB.txt	B.higgsFit.svg

probA: out.logA.txt

B.higgsFit.svg: probB/B.higgsFit.svg
	cp $< $@

out.logB.txt: probB/out.logB.txt
	cp $< $@

probB/out.logB.txt probB/B.higgsFit.svg: probB/Makefile
	cd probB/ && make

out.logA.txt: probA/out.logA.txt
	cp $< $@

probA/out.logA.txt: probA/Makefile
	cd probA/ && make

.PHONY:clean
clean:
	cd probA/ && make clean
	cd probB/ && make clean
	rm -f *.svg out.*.txt
