all: A-IntegratorComparison.svg B-SIRModel.svg C-Figure8.svg

C-Figure8.svg: probC/C-Figure8.svg
	cp probC/C-Figure8.svg ./

probC/C-Figure8.svg: probC/Makefile
	cd probC/ && make

B-SIRModel.svg: probB/B-SIRModel.svg
	cp probB/B-SIRModel.svg ./

probB/B-SIRModel.svg: probB/Makefile
	cd probB/ && make

A-IntegratorComparison.svg: probA/A-IntegratorComparison.svg
	cp probA/A-IntegratorComparison.svg ./

probA/A-IntegratorComparison.svg: probA/Makefile
	cd probA && make

.PHONY:clean
clean: 
	cd probA/ && make clean
	cd probB/ && make clean
	cd probC/ && make clean
	rm -f A-*.svg B-*.svg C-*.svg
