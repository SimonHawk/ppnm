all: out.txt ExC.svg
	cat out.txt

ExC.svg: exC.gpi out.exC.txt
	gnuplot	$<

out.txt: main.exe out.exC.txt
	mono $< > out.txt

main.exe: main.cs matlib.dll
	mcs -out:$@ -reference:matlib.dll $<

LIBDIR = ../../lib/matlib
SRC = $(LIBDIR)/integration/quado8.cs
SRC += $(LIBDIR)/integration/quado4.cs
matlib.dll: $(SRC)
	mcs -target:library -out:$@ $^

.PHONY:clean
clean:
	rm -f *.dll *.exe out.txt
