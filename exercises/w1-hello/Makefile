all: out.helloYou.txt out.hello.txt
	cat out.hello.txt
	cat out.helloYou.txt

out.helloYou.txt: helloYou.exe
	mono $< > $@

helloYou.exe: helloYou.cs 
	mcs -out:$@ $< 

out.hello.txt: hello.exe
	mono $< > $@

hello.exe: hello.cs
	mcs -out:$@ $<
