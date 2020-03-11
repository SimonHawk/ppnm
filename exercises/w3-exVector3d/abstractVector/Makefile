all:out.txt
	cat out.txt

out.txt: main.exe
	mono main.exe > out.txt

main.exe:main.cs avector.dll vector_field.dll
	mcs -reference:avector.dll,vector_field.dll -out:$@ $<

vector_field.dll: vector_field.cs avector.dll
	mcs -target:library -reference:$(word 2, $^) -out:$@ $<

avector.dll: avector.cs
	mcs -target:library -out:$@  $^

.PHONY:clean

clean:
	rm -f avector.dll
