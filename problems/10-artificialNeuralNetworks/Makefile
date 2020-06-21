all: probA probB probC

# Commented out probC, as it doesn't work well, and is very slow.
# probC: out.logC.txt C.ODE.svg

probB: out.logB.txt B.interpolation.svg

probA: out.logA.txt A.interpolation.svg

out.logC.txt: probC/out.logC.txt
	cp $< $@

C.ODE.svg: probC/C.ODE.svg
	cp $< $@

probC/out.logC.txt probC/C.ODE.svg: probC/Makefile
	cd probC/ && make

out.logB.txt: probB/out.logB.txt
	cp $< $@

B.interpolation.svg: probB/B.interpolation.svg
	cp $< $@

probB/out.logB.txt probB/B.interpolation.svg: probB/Makefile
	cd probB && make

out.logA.txt: probA/out.logA.txt
	cp $< $@

A.interpolation.svg: probA/A.interpolation.svg
	cp $< $@

probA/out.logA.txt probA/A.interpolation.svg: probA/Makefile
	cd probA/ && make


.PHONY:clean
clean:
	cd probA/ && make clean
	cd probB/ && make clean
	# cd probC/ && make clean
	rm -f *.svg out.*.txt


