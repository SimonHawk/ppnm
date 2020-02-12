all: main.exe 
	mono main.exe

main.exe: main.cs
	mcs -out:$@ $^

.PHONY:clean
clean:
	rm -f main.exe
