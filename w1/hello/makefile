# How to make a makefile:

# The final output we want is out.txt. To make this we need the "ingedient"
# hello.exe.
# out.txt is made by runnig mono on hello.exe and putting the result in 
# out.txt
out.txt : hello.exe
	mono hello.exe > out.txt

# To make hello.exe, we need hello.cs
# This is done by running mcs on hello.cs and putting the output in
# hello.out
# The -out flag is an mcs option, -out:hello.exe renames the output file of
# the mcs run to hello.exe.
hello.exe : hello.cs
	mcs hello.cs -out:hello.exe

# The make is done now, but we want to clean up. This should not be done
# every time we run the makefile, but define how it should be done.
# To define such a command that is not run, we make it a target or
# "ingredient" of the .PHONEY tag. Then it can exist on it's own.
.PHONEY:clean

# To make clean, we don't need anything
# To make it, remove out.txt and hello.exe
# This clean command can be run by running "make clean".
clean:
	rm --force out.txt hello.exe	

# Thereby we are done.
