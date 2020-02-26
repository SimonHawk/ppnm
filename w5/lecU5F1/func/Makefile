all: out.txt
	cat out.txt

out.txt: main.exe
	mono $< > out.txt 

main.exe: main.cs matlib.dll
	mcs -o:$@ -reference:matlib.dll $<

LIBDIR = ../../../lib/matlib
SRC = $(LIBDIR)/integration/quado8.cs
SRC += $(LIBDIR)/integration/quado4.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@ 
