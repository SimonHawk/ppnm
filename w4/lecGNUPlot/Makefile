Gamma.svg: gamma.gpi out.gamma.txt out.gamma.test.txt
	gnuplot $<

out.gamma.txt: main-gamma.exe
	mono $< > $@

out.gamma.test.txt: gamma.c
	tcc -run gamma.c > out.gamma.test.txt

main-gamma.exe: main-gamma.cs math.dll
	mcs -reference:math.dll -out:$@ $< 

math.dll: gamma.cs
	mcs -target:library -out:$@  $<

.PHONY:clean
clean:
	rm -f *.dll *.exe out* *.svg
	
	
