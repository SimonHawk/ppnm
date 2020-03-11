all:out.txt
	# Put the contents of out.txt into the console.
	cat out.txt

out.txt: main.exe
	# Run the executable and put the result in the file out.txt
	mono main.exe > out.txt

main.exe: main.cs cmath.dll
	# Compile an executable, called the same as the recipie target
	# including the library cmath.dll from the source file main.cs
	mcs -out:$@ -reference:cmath.dll main.cs

cmath.dll: complex.cs cmath.cs
	# compile a library called the same as the recipie target using
	# all recipie prerequisits:
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	# Remove the intermediate files. 
	rm -f out.txt main.exe  cmath.dll
	
