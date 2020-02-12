# Smarte macros:
#	$@: target navnet, f.eks. cmath.dll
#	$^: alle prerequisits ("ingredienser")
#	$<: første prerequisit

main.exe:main.cs cmath.dll
	mcs -out:$@ -reference:cmath.dll  main.cs

cmath.dll:cmath.cs complex.cs
	mcs -target:library -out:$@ $^


