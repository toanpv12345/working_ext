/**
 **
 **  Security Examples
 **  CRC-8 calculation of a plaintext key ring  
 **
 **  ======================================================================== \n
 **  Copyright 2016 by 3M Company. All Rights Reserved.                       \n
 **  ======================================================================== \n
 */
#include "stdio.h"
#include "stdlib.h"
#include "string.h"
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <errno.h>
#include "crc8.h"

#define NUM_KEYS 256
#define MAX_KEY_SIZE (4096/8)
#define MAX_KEY_RING_SIZE (NUM_KEYS * MAX_KEY_SIZE)


int main(int argc, char *argv[]) {
    int key_fd, inlen;
    unsigned char inbuf[2*MAX_KEY_RING_SIZE];
    char *key_file;
    unsigned int crc;

    if (argc != 2) {
        printf("compute_crc8 key_ring_file\n");
        exit(-1);
    }

    // This is the plain text key ring file
    key_file = argv[1];

    printf("opening files\n");

    // open key file...
    key_fd = open(key_file, O_RDONLY);
    if (key_fd < 0) {
        printf("Error openning input key ring file\n");
        close(key_fd);
        exit(-1);
    }

    // Read in key file and write it out...
    inlen = read(key_fd, inbuf, sizeof(inbuf));
    if (inlen == -1 || errno == EINTR) {
        printf("Error reading from input key ring file\n");
        close(key_fd);
        exit(-1);
    }

    printf("Read %d bytes from input key ring\n", inlen);

    // Generate CRC of the unecrypted ring
    /* Perform cyclic redundancy check computation */
    crc = compute_crc8(inbuf, inlen);
    printf("crc of plain text key ring: 0x%02X\n", crc);

    close(key_fd);
    return 0;
}
