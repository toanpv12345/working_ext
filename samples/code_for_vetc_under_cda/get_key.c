/**
 **
 **  Security functions
 **
 **  Get a key from a plain text key ring 
 **
 **  ======================================================================== \n
 **  Copyright 2016 by 3M Company. All Rights Reserved.                       \n
 **  ======================================================================== \n
 */
#include <openssl/evp.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <errno.h>
#include <unistd.h>
#include <openssl/pem.h>
#include <openssl/bio.h>
#include <openssl/rsa.h>
#include <openssl/evp.h>
#include <openssl/sha.h>

int main(int argc, char *argv[]) {
    unsigned char *key_file, key[4096/8];
    unsigned long key_size, key_index;
    struct stat stats;
    int key_fd, len, i;

    if (argc != 4) {
        printf("get_key key_ring_file key_size key_index\n");
        exit(-1);
    }

    key_file = argv[1];
    key_size = strtoul(argv[2], NULL, 10);
    if (key_size < 32 || key_size > 4096) {
        printf("key ring size too small or too big: %d\n", key_size);
        exit(-1);
    }
    key_index = strtoul(argv[3], NULL, 10);
    if (key_index > 256) {
        printf("key index too small or too big: %d\n", key_index);
        exit(-1);
    }

    if (stat(key_file, &stats) != 0) {
        printf("key_file does not exist\n");
        exit(-1);
    }

    if (stats.st_size != (key_size/8*256)) {
        printf("key_file size should be %d, is %d\n", (key_size/8*256), stats.st_size);
        exit(-1);
    }

    // open key file...
    key_fd = open(key_file, O_RDONLY);
    if (key_fd < 0) {
        perror("key_file open error");
        exit(-1);
    }

    len = lseek(key_fd, (key_index*key_size/8), SEEK_SET);
    if (len != (key_index*key_size/8)) {
        perror("lseek failure");
        printf("lseek cnt = %d\n", len);
        exit(-1);
    }

    len = read(key_fd, key, (key_size/8));
    if (len != (key_size/8)) {
        perror("read failure");
        printf("  len = %d\n", len);
        exit(-1);
    }

    printf("key:    ");

    for (i=0; i<len; i++) {
        printf("%02X", key[i]);
    }
    printf("\n");


}
