all: output.txt
	cat output.txt

output.txt:mainStdIn.exe input.txt
	mono mainStdIn.exe < input.txt > output.txt

mainStdIn.exe: mainStdIn.cs
	mcs -out:$@ $^

