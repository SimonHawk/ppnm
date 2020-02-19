all: out.txt out-stdin.txt

out.txt: cmdline.exe input.txt Makefile
	mono $< 1 2 3 1> $@
	mono $< $$(cat input.txt) 1>> $@
	
out-stdin.txt error-stdin.txt: stdin.exe input.txt Makefile
	mono $< < input.txt 1> out-stdin.txt 2> error-stdin.txt
	# The wrong way to do it is: (Because abuse of the cat command)
	# cat input.txt | mono $< > out.txt

cmdline.exe:cmdline.cs
	mcs -out:$@ $<

stdin.exe: stdin.cs
	mcs -out:$@ $<

input.txt: Makefile 
	# Here we use -e to interpret \n as \n and not as 
	/bin/echo -e '0.5\n2.5\n3.5' > $@

.PHONY:clean
clean:
	$(RM) *.exe out*

