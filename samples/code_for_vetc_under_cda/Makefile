#
#  Security Examples
#
#  ========================================================================
#  Copyright 2016 by 3M Company. All Rights Reserved.                      
#  ========================================================================
#
all: genpass example get_key compute_crc8

get_key: get_key.c
	gcc -g -o get_key get_key.c -lssl

genpass: genpass.c iop_security.h
	gcc -g -o genpass genpass.c -lssl

example: example.c
	gcc -g -o example example.c -lssl

compute_crc8: crc8.h crc8.c compute_crc8.c
	gcc -g -c crc8.c
	gcc -g -c compute_crc8.c
	gcc -o compute_crc8 crc8.o compute_crc8.o

clean:
	rm *.o
	rm -f genpass
	rm -f example
	rm -f get_key
	rm -f compute_crc8
