# Define a flag we can use in the mcs command to say that we optimze for
# the arm architecture.
CSFLAGS = -optimize+ -platform:anycpu

.PHONY:all
all: out.txt
	cat out.txt

out.txt:main.exe
	mono main.exe > out.txt

main.exe:main.cs
	mcs $(CSFLAGS)  -out:main.exe main.cs

# Define that clean is not a file, so even if a file called clean exists
# the commands found in clean will be executed. 
.PHONY:clean

clean:
	rm --force out.txt main.exe
