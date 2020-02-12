all:out.txt
	cat out.txt

out.txt: main.exe
	mono main.exe > out.txt

main.exe: main.cs ivector3d.dll  vector3d.dll vector3d_array.dll
	mcs -reference:vector3d.dll,ivector3d.dll,vector3d_array.dll -out:$@  $<

vector3d_array.dll: vector3d_array.cs ivector3d.dll
	mcs -target:library -reference:$(word 2, $^)  -out:$@ $<

vector3d.dll: vector3d.cs ivector3d.dll
	mcs -target:library -reference:$(word 2, $^)  -out:$@ $<

ivector3d.dll: ivector3d.cs
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f out.txt main.exe vector3d.dll ivector3d.dll
