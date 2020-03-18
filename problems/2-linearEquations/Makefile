all:out.A.txt out.B.txt out.C.txt
	cat out.A.txt
	cat out.B.txt
	cat out.C.txt

out.C.txt: probC/out.C.txt
	cp $< $@

probC/out.C.txt: probC/Makefile
	cd probC && make out.C.txt

out.B.txt: probB/out.B.txt
	cp $< $@

probB/out.B.txt: probB/Makefile
	cd probB && make out.B.txt


out.A.txt: probA/out.A.txt
	cp $< $@

probA/out.A.txt: probA/Makefile
	cd probA && make out.A.txt

.PHONY:clean
clean:
	cd probA && make clean
	cd probB && make clean
	cd probC && make clean
	rm -f out.*.txt	
