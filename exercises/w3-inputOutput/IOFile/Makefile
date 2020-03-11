outputfile: mainIOFile.exe inputfile
	mono $< inputfile $@

mainIOFile.exe: mainIOFile.cs
	mcs -out:$@ $^	

.PHONY:clean
clean:
	rm -f mainIOFile.exe

