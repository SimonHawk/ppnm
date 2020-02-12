all: out.txt
	cat out.txt

out.txt: helloName.exe
	mono helloName.exe > out.txt

helloName.exe: helloName.cs
	mcs -out:$@ $^

.PHONY:clean
clean:
	rm -f out.txt hello.exe
