/** 
 **
 **  Security functions
 **
 **  Generate password from UII and key 
 **
 **  ======================================================================== \n
 **  Copyright 2016 by 3M Company. All Rights Reserved.                       \n
 **  ======================================================================== \n
 */

#ifndef _IOP_SECURITY_C
#define _IOP_SECURITY_C

/****************************************************************************/ 
/*                                                                          */ 
/*                      MODULES USED                                        */ 
/*                                                                          */ 
/****************************************************************************/ 

#include <openssl/evp.h>
#include "iop_security.h"

/****************************************************************************/ 
/*                                                                          */ 
/*                      DEFINITIONS AND MACROS                              */ 
/*                                                                          */ 
/****************************************************************************/ 

static EVP_MD_CTX mdctx;
static const EVP_MD *mdtype = NULL;

/****************************************************************************/ 
/*                                                                          */ 
/*                      EXPORTED FUNCTIONS                                  */ 
/*                                                                          */ 
/****************************************************************************/ 

int iop_security_init(int phase) {
    if (phase == 0) {
        EVP_MD_CTX_init(&mdctx);
        mdtype = EVP_sha1();
    }
    return 0;
}

// Returns 1 for success, 0 on failure
// Make sure md has at least 20 bytes allocated (160 bits for SHA1)
int iop_security_sha1(const char *uii, int uii_len, 
                      const char *key, int key_len, 
                      char *md, int *md_len) {

    int rc;

    // All below return 1 on success, 0 on failure...
    rc = EVP_DigestInit_ex(&mdctx, mdtype, NULL);
    if (key_len <= 4) {
        // Original way of generating hash
        rc &= EVP_DigestUpdate(&mdctx, uii, uii_len);
        rc &= EVP_DigestUpdate(&mdctx, key, key_len);
    } else {
        // normal HMAC way (putting key first)...  No more secure, but allows 
        // easier map to other approaches...
        rc &= EVP_DigestUpdate(&mdctx, key, key_len);
        rc &= EVP_DigestUpdate(&mdctx, uii, uii_len);
    }
    rc &= EVP_DigestFinal_ex(&mdctx, md, md_len);

    return rc;

}

int main(int argc, char *argv[]) {
    unsigned char *inuii, uii[64], *inkey, key[64], md[20], tmp_char[10];
    int rc, uii_len, key_len, i, md_len;
    unsigned long conv_tmp;

    if (argc != 3) {
        printf("usage: genpass <uii> <key>\n");
        exit(-1);
    }

    inuii = argv[1];
    uii_len = strlen(inuii);
    inkey = argv[2];
    key_len = strlen(inkey);

    // Convert ascii to binary
    for (i=0; i<key_len/2; i++) {
        memcpy(tmp_char, &inkey[2*i], 2);
        tmp_char[2] = '\0';
        conv_tmp = strtoul(tmp_char, NULL, 16);
        if (conv_tmp < 0 || conv_tmp > 255) {
            printf("invalid hex character in key_string, position %d\n", 2*i);
            exit(-1);
        }
        key[i] = (unsigned char ) (conv_tmp & 0xff);
    }

    // Convert ascii to binary
    for (i=0; i<uii_len/2; i++) {
        memcpy(tmp_char, &inuii[2*i], 2);
        conv_tmp = strtoul(tmp_char, NULL, 16);
        if (conv_tmp < 0 || conv_tmp > 255) {
            printf("invalid hex character in uii, position %d\n", 2*i);
            exit(-1);
        }
        uii[i] = (unsigned char ) (conv_tmp & 0xff);
    }

    rc = iop_security_init(0);
    if (rc != 0) {
        printf("init failure\n");
        exit(-1);
    }

    rc = iop_security_sha1(uii, uii_len/2, key, key_len/2, md, &md_len);
    if (rc != 1) {
        printf("passgen failure\n");
        exit(-1);
    }

    for (i=0; i<md_len; i++) {
        printf("%02X", md[i]);
    }
    printf("\n");

    for (i=8; i<12; i++) {
        printf("%02X", md[i]);
    }
    printf("\n");

}


#endif // _IOP_SECURITY_C
