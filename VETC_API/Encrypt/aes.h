#pragma once

#ifndef uint8
#define uint8  unsigned char
#endif

#ifndef uint32
#define uint32 unsigned long int
#endif

struct aes_context
{
	uint32 erk[64];     /* encryption round keys */
	uint32 drk[64];     /* decryption round keys */
	int nr;             /* number of rounds */
};

class AES
{
public:
	AES();
	~AES();
	int  setKey(aes_context *ctx, uint8 *key, int nbits);
	void encrypt(aes_context *ctx, uint8 input[16], uint8 output[16]);
	void decrypt(aes_context *ctx, uint8 input[16], uint8 output[16]);

private:
	void aes_gen_tables(void);

private:
	int do_init;
	int KT_init;
};

