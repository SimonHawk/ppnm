outputStdIn.txt:mainStdIn.exe input.txt
	mono mainStdIn.exe < input.txt > $@ 

mainStdIn.exe: mainStdIn.cs
	mcs -out:$@ $^

.PHONY:clean
clean:
	rm -f outputStdIn.txt mainStdIn.exe
