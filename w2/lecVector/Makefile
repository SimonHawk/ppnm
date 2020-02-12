all:out.txt
	cat out.txt

out.txt: main.exe
	mono main.exe > out.txt

main.exe: main.cs vector3d.dll
	mcs -reference:$(word 2, $^) -out:$@ main.cs

vector3d.dll: vector3d.cs
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f vector3d.dll main.exe out.txt
