Gamma.svg: gamma.gpi out.gamma.txt
	gnuplot $<

out.gamma.txt: main-gamma.exe
	mono $< > $@

main-gamma.exe: main-gamma.cs math.dll
	mcs -reference:math.dll -out:$@ $< 

math.dll: gamma.cs
	mcs -target:library -out:$@  $<

.PHONY:clean
clean:
	rm -f *.dll *.exe out* *.svg
	
	
