Overview:
    In order to create a solution for calculating secure tag passwords, the following high level steps need to be completed:

Step1:
    Select langauge and software technologies for use in password utility. Current utilities are written in C.

Step2:
    Set up development environment for targeted software technologies.

Step3:
    Extract code files from this package and convert to target software technoligies as appropriate.

Step4:
    Obtain openssl library for targeted software technologies.

Step5:
    Extract and port Makefile in this package to targeted software technologies.

Step6:
    Compile and test. To compile, the libraries used are openssl. Depending on platforms used to compile the code, different library options may need to be used. It could be "-lssl", "-lcrypto", "-lssleay32 -llibeay32" or others. The include path for the openssl library include files also needs to be provided to the compiler.


Utility functions:

genpass:
    Will take in a UII (TID in most cases) and a KEY and generate the password
    like we do on the readers.  Inputs are in ASCII hex strings (no 0x
    preceeding them), and will be converted to binary form before performing the
    SHA1 digest.
    example: 
      >./genpass 112233445566778899001122 4F89F9FC9D47762096C1805CA163F07AB66FC6DA53678878A612424CCDE1E1A1
      35F207D6A83471B2FAB15CA5A81C0101E71DDF54 (the 20 byte SHA-1 hash from above)
      FAB15CA5 (password is the 4 bytes after the leading 8 bytes)
    where the 1st parameter is the UII(TID) and the 2nd parameter is the 32 byte key 
	hash1
	md5(tid+hÁsh1). 2byte from 9
get_key:
    Will take in a binary key ring file along with information about the key
    size and which index is desired and spit out the key in ASCII.  This key can
    then be used in genpass along with the TID to generate a password for a
    particular tag.
    example: 

      >./get_key Your_Plaintext_Key_Ring 256 1
      key:    DB5105A0B63F1D75CCF0A7EB9264CE8D48AB4110DA9CAC87E7D2FA198C351F93

    where 256 is the key size in bits (256/8=32 bytes) and 1 is the key index (0-255)

example:
    Just an example showing how passwords are generated from UII (TID in most cases) 
    and a key hard coded in program. 

      >./example
      sha1 digest: 9DAC294CEEC0039F07BE090DD0F0CC2D3369978D
      password:    07BE090D

    The "example" program (command) hard codes a UII and a key, it then generates the 20 byte
    hash and take the 4 bytes after the leading 8 bytes as the password

compute_crc8:
    This program takes input of a plain text key ring file (8KB size) and compute the 8 bit CRC
    and print it in hex. 

    > ./compute_crc8 Your_Plaintext_Key_Ring 
    opening files
    Read 8192 bytes from input key ring
    crc of plain text key ring: 0xB8