/**
 **
 **  Security Examples
 **  CRC-8 routines 
 **
 **  ======================================================================== \n
 **  Copyright 2016 by 3M Company. All Rights Reserved.                       \n
 **  ======================================================================== \n
 */
#include <stdio.h>

#undef bool
typedef unsigned char bool;
#undef FALSE
#undef TRUE
#define FALSE 0
#define TRUE  1

/* Define your polynomial generator here */
/* 
   CRC-8-CCITT     ---         x^8 + x^2 + x^1 + x^0
   CRC-8           ---         x^8 + x^7 + x^6 + x^4 + x^2 + x^0
   CRC-8-SAE J1850 ---         x^8 + x^4 + x^3 + x^2 + x^0
   CRC-8-WCDMA     ---         x^8 + x^7 + x^4 + x^3 + x^1 + x^0 
 */
/* #define CRC_POLY 0x11d  */         /* g(x) = 0x11d = (1 0001 1101)(bin) = x^8 + x^4 + x^3 + x^2 + x^0 */
#define CRC_POLY 0x107          /* g(x) = 0x107 = (1 0000 0111)(bin) = x^8 + x^2 + x^1 + x^0*/
#define CRC_TBL_SIZE 256 

/* CRC lookup table*/
static unsigned char crc8_table[CRC_TBL_SIZE];  /* Note: Currently this is hardcoded for an 8Bit CRC computation */

bool crc_debug = FALSE;         /* Debug mode? */
static bool crc_table_initialized = FALSE;

/*
 Helper function:
  Polynomial multiplication algorithm (mult(poly, mul) = mul * poly),
  with coefficients added modulo 2.
  The reulting 16Bit value is in the form hi(b):lo(b)
*/
static unsigned short mult(unsigned short crc_polynomial, unsigned char mul) 
{

    unsigned short rem = crc_polynomial;
    unsigned short result = 0;

    for (; mul != 0; mul >>= 1, rem <<= 1)
        if (mul & 1) result ^= rem;

    return result;
}

/*******************************************
*  Generate CRC8 lookup table from a given
* 8th order polynomial generator.
*******************************************/
void generate_crc8_table(unsigned short crc_polynomial) 
{

    unsigned char rem[CRC_TBL_SIZE];    /* Helper arrays */
    unsigned char quot[CRC_TBL_SIZE];
    unsigned short i;   
    unsigned short w;

    if(crc_table_initialized)
        return;

    for (i = 0; i < CRC_TBL_SIZE; i++) 
    {
        w = mult(crc_polynomial, (unsigned char)i);
        quot[i] = w >> 8;   /* Seperate high and low byte to be treated as */
        rem[i] = (unsigned char)w;  /* 'quotient' and remainder */
    }

    /* Compose table by finding the apropriate remainders to a given quotient */
    for (i = 0; i < CRC_TBL_SIZE; i++) 
    { 
        crc8_table[i] = rem[quot[i]];
        /* Dump CRC lut when in debug mode */
        /* printf("%d: %2x, \n",i,table[i]); */
        if (crc_debug) printf("%d: %2x, \n",i,crc8_table[i]);
        
    }

    crc_table_initialized = TRUE;
}

/*******************************************
* Compute 8 bit CRC provided table, data and length
*******************************************/
static unsigned char run_crc8(unsigned char *buf, int len) 
{

    unsigned short crc=0;

    /* This is where the actual CRC computation will be performed using
       a 16Bit workspace to speed up a little. 
    */
    while (len--) {
        crc = ((crc << 8)|*buf++) ^ crc8_table[(unsigned char)crc];

        /* Trace CRC computation when in debug mode*/
        if (crc_debug) printf("\nCRC = %2x, remainder = %2x, *buf = %2x", (unsigned char)crc, (unsigned char)crc8_table[crc >> 8], buf[-1]);
    }

    if (crc_debug) printf("\n");

    return (unsigned char)crc;  /* Return least significant byte as CRC8 value */
}


/* Compute the CRC */
unsigned char compute_crc8(unsigned char *buf, int len)
{
    unsigned char crc;

    if(!crc_table_initialized)
    {
        /* Tabel must exist first */
        generate_crc8_table(CRC_POLY); 
    }

    crc = run_crc8(buf, len);

    return crc;
}

