all: outCommandLineArgs.txt
	cat $<

outCommandLineArgs.txt: main.exe 
	mono main.exe 0.1 0.2 0.3 0.5 1 2 > $@

main.exe: main.cs
	mcs -out:$@ $^

.PHONY:clean
clean:
	rm -f main.exe outCommandLineArgs.txt
