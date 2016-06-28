/**
 **
 **  Security Examples
 **
 **  ======================================================================== \n
 **  Copyright 2016 by 3M Company. All Rights Reserved.                       \n
 **  ======================================================================== \n
 */

/****************************************************************************/ 
/*                                                                          */ 
/*                      MODULES USED                                        */ 
/*                                                                          */ 
/****************************************************************************/ 

#include <openssl/evp.h>

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

void security_init(void) {
    EVP_MD_CTX_init(&mdctx);
    mdtype = EVP_sha1();
}

// Returns 1 for success, 0 on failure
// Make sure md has at least 20 bytes allocated (160 bits for SHA1)
int security_sha1(const char *uii, int uii_len, 
                      const char *key, int key_len, 
                      char *md, int *md_len) {

    int rc;

    // All below return 1 on success, 0 on failure...
    rc = EVP_DigestInit_ex(&mdctx, mdtype, NULL);
    rc &= EVP_DigestUpdate(&mdctx, key, key_len);
    rc &= EVP_DigestUpdate(&mdctx, uii, uii_len);
    rc &= EVP_DigestFinal_ex(&mdctx, md, md_len);

    return rc;

}

int main(int argc, char *argv[]) {
    unsigned char md[20];
    int rc, uii_len, key_len, i, md_len;
    unsigned long conv_tmp;
    unsigned char uii[] = {0xE2, 0x00, 0x34, 0x12, 0x01, 0x2E, 0xC0, 0xFF, 0xEE,
                           0x04, 0x13, 0x92, 0x29, 0x19, 0x00, 0x58, 0x00, 0x04,
                           0x5F, 0xFB, 0xFF, 0xFF, 0xDC, 0x60};
    unsigned char key[] = {0xC1, 0x29, 0x87, 0x83, 0x20, 0xC2, 0xD8, 0xF3, 0x14,
                           0x31, 0xAD, 0x04, 0x11, 0x58, 0x65, 0xE9};

    uii_len = sizeof(uii);
    key_len = sizeof(key);

    security_init();

    rc = security_sha1(uii, uii_len, key, key_len, md, &md_len);
    if (rc != 1) {
        printf("passgen failure\n");
        exit(-1);
    }

    printf("sha1 digest: ");
    for (i=0; i<md_len; i++) {
        printf("%02X", md[i]);
    }
    printf("\npassword:    ");

    for (i=8; i<12; i++) {
        printf("%02X", md[i]);
    }
    printf("\n");

}

