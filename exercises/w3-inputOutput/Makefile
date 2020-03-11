all:stdIn/outputStdIn.txt commandLineArgs/outCommandLineArgs.txt IOFile/outputfile
	echo "Using standard input streams\n"
	cat stdIn/outputStdIn.txt
	echo "\nUsing Command line Args\n"
	cat commandLineArgs/outCommandLineArgs.txt
	echo "\nUsing file streams\n"
	cat IOFile/outputfile

stdIn/outputStdIn.txt: stdIn/Makefile
	cd stdIn/ && make

commandLineArgs/outCommandLineArgs.txt: commandLineArgs/Makefile
	cd commandLineArgs/ && make

IOFile/outputfile: IOFile/Makefile
	cd IOFile/ && make

.PHONY:clean
clean:
	cd stdIn/ && make clean
	cd commandLineArgs/ && make clean
	cd IOFile/ && make clean
