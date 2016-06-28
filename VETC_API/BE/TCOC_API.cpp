#include "TCOC_API.h"
#include <process.h>
#include "../Crypto/aes.h"
#include "../Crypto/sha.h"
#include "../Crypto/ccm.h"
//#include <future>
#include <iostream>

#include <cstdlib>
using std::exit;

#include "../Crypto/cryptlib.h"
using CryptoPP::Exception;

#include "../Crypto/hex.h"
using CryptoPP::HexEncoder;
using CryptoPP::HexDecoder;

#include "../Crypto/filters.h"
using CryptoPP::StringSink;
using CryptoPP::StringSource;
using CryptoPP::StreamTransformationFilter;

using CryptoPP::AES;
using CryptoPP::CBC_Mode;

using namespace std;

TCOC_API::TCOC_API(const char* serverIP, int serverPort, TCOCCallback callBack)
{
	const char * account = "vetc";
	const char * pwd = "vetc@123";
	//uint8 encriptKey[16] = { 'F','C','9','D','C','4','1','5','6','6','8','C','F','4','4','B' };

	m_staion = 460;
	memset(m_account, 0, sizeof(m_account));
	memset(m_pwd, 0, sizeof(m_pwd));

	memcpy(m_account, account, strlen(account));
	memcpy(m_pwd, pwd, strlen(pwd));

	_callBack = callBack;
	m_enableReceive = false;
	m_requestID = 0;
	m_sessionID = 0;

	char port[10];
	sprintf_s(port, sizeof(port), "%d", serverPort);

	/*m_network = new Network(serverIP, port);
	if (m_network->connectBE())
	{
		m_enableReceive = true;
		_beginthread(&TCOC_API::startReceiveWrapper, 0, this);
		this->Connect();
	}*/

	this->testAES();
}

TCOC_API::~TCOC_API(void)
{

}

void TCOC_API::Connect()
{
	ConnectCmd connectCmd;
	connectCmd.make(m_account, m_pwd, m_staion, 200, m_requestID, 0);

	char data[sizeof(ConnectCmd)];
	memcpy(data, &connectCmd, sizeof(ConnectCmd));

	for (int i = 0; i < sizeof(ConnectCmd); i++)
		printf("%2x", data[i]);

	printf("  ConnectCmd\n");

	char buff[sizeof(ConnectCmd)];
	connectCmd.serialize(buff);
	//memcpy_s(buff, sizeof(ConnectCmd), data, sizeof(ConnectCmd));
	//m_aes->encrypt(&m_aesContext, buff, buff);

	for (int i = 0; i < sizeof(ConnectCmd); i++)
		printf("%2x", buff[i]);

	printf("  ConnectCmd Encrypt\n");
	/*char* dataEncrypt = this->encrypt(data, sizeof(ConnectCmd));
	int len = strlen(dataEncrypt);

	printf("\ndataEncrypt:");
	for (int i = 0; i < len; i++)
		printf("%2x", dataEncrypt[i]);

	printf("\ndataDecrypt:");
	char* dataDecrypt = this->decrypt(data, strlen(data));
	len = strlen(dataDecrypt);
	for (int i = 0; i < len; i++)
		printf("%2x", dataDecrypt[i]);*/

	m_network->sendPackets((char*)buff, sizeof(ConnectCmd));
}

void TCOC_API::receivePacketWithEncrypt()
{
	//Packet packet;
	char packet[MAX_PACKET_SIZE] = { 0 };
	int data_length = m_network->receivePackets(packet);

	if (data_length <= 0)
		return;

	for (int i = 0; i < data_length; i++)
		printf("%2x ", packet[i]);

	printf("  packet\n");

	/*char* packetDecrypt = decrypt(packet + 4, data_length);
	printf("packetDecrypt:");
	int pl = strlen(packetDecrypt);
	for(int i = 0; i < pl; i++)
		printf("%2x", packetDecrypt[i]);

	printf("\n");*/

	char buff[100];
	//memcpy_s(buff, data_length - 4, packet + 4, data_length - 4);

	//m_aes->decrypt(&m_aesContext, buff, buff);

	for (int i = 0; i < data_length - 4; i++)
		printf("%2x ", buff[i]);

	printf("  packetDecrypt\n");

	if (_callBack != nullptr)
		_callBack((char*)buff, data_length);
}

void TCOC_API::sendPacketWithEncrypt(char * packet, int packetSize)
{
	if (packet == nullptr || packetSize <= 0)
		return;

	char* packetEncrypt = encrypt(packet, packetSize);
	m_network->sendPackets(packetEncrypt, strlen(packetEncrypt));
}

void TCOC_API::startReceive()
{
	while (m_enableReceive)
	{
		receivePacketWithEncrypt();
	}
}

void TCOC_API::startReceiveWrapper(void *p)
{
	static_cast<TCOC_API*>(p)->startReceive();
}

char * TCOC_API::encrypt(char * packet, int packetSize)
{
	//unsigned char *buff = new unsigned char[packetSize];
	/*uint8 buff[100];
	memcpy_s(buff, packetSize, packet, packetSize);
	m_aes->encrypt(&m_aesContext, buff, buff);

	int len = strlen((const char*)buff);
	char *dataEncrypt = new char[len + sizeof(int)];
	memcpy(dataEncrypt, &len, sizeof(int));
	memcpy(dataEncrypt + 4, buff, len);

	return dataEncrypt;*/
	return nullptr;
}

char * TCOC_API::decrypt(char * packetEncript, int packetSize)
{
	//unsigned char *buff = new unsigned char[packetSize];
	/*uint8 buff[100];
	memcpy_s(buff, packetSize, packetEncript, packetSize);
	m_aes->decrypt(&m_aesContext, buff, buff);
	return (char*)buff;*/
	return nullptr;
}

void TCOC_API::testAES()
{
	//int n = 0, i = 0;
	//aes_context ctx;
	//AES *aes = new AES();
	//uint8 buf[44];
	//uint8 key[256];
	////2c 0 0 0 0 0 0 0 1000000076657463000000766574634031323300cc100e8300
	////static uint8 AES_test[44] = { 0x2c,0,0,0,0,0,0,0,0x01,0,0,0,0,0,0,0,0x76,0x65,0x74,0x63,0,0,0,0,0,0,0x76,0x65,0x74,0x63,0x40,0x31,0x32,0x33,0,0,0xcc,0x01,0,0,0xe8,0x03,0,0 };
	//char AES_test[sizeof(ConnectCmd)];
	//ConnectCmd connectCmd;
	//connectCmd.make("vetc", "vetc@123", 460, 1000, 0, 0);
	//connectCmd.serialize(AES_test);

	//char *secret_key = "FC9DC415668CF44B";
	//memcpy(key, secret_key, strlen(secret_key));

	//for (i = 0; i < strlen(secret_key); i++) {
	//	printf("%2x ", key[i]);
	//}
	//printf(" (Key)\n\n");

	///* Keys can be 128 bits, 192 bits, and 256 bits  */
	////for (n = 0; n < 3; n++)
	//{
	//	printf("\n-------------------------------------------------\n");
	//	printf("Test %d: key size = %3d bits\n", n, 128 + n * 64);

	//	fflush(stdout);

	//	/* Set the plain-text */
	//	memcpy(buf, AES_test, sizeof(AES_test));

	//	/* Set the key */
	//	memcpy(key, secret_key, strlen(secret_key) + n * 8);
	//	aes->setKey(&ctx, key, 128 + n * 64);

	//	/* Print out the plain text */
	//	for (i = 0; i < sizeof(buf); i++) {
	//		printf("%2x ", buf[i]);
	//	}
	//	printf(" (Plain Text)\n\n");

	//	aes->encrypt(&ctx, buf, buf);
	//	for (i = 0; i < sizeof(buf); i++) {
	//		printf("%2x ", buf[i]);
	//	}
	//	printf(" (Cipher Text)\n\n");


	//	aes->decrypt(&ctx, buf, buf);
	//	for (i = 0; i < sizeof(buf); i++) {
	//		printf("%2x ", buf[i]);
	//	}
	//	printf(" (Decrypted Plain Text)\n\n");
	//}

	//Key and IV setup
	//AES encryption uses a secret key of a variable length (128-bit, 196-bit or 256-   
	//bit). This key is secretly exchanged between two parties before communication   
	//begins. DEFAULT_KEYLENGTH= 16 bytes

	//CTR_Mode<AES>::Encryption aes(key, key.size(), iv);

	byte key[AES::DEFAULT_KEYLENGTH];
	memcpy(key, "FC9DC415668CF44B", AES::DEFAULT_KEYLENGTH);

	byte iv[AES::BLOCKSIZE];
	memset(iv, 0, AES::BLOCKSIZE);

	char cmd[sizeof(ConnectCmd)];;
	ConnectCmd connectCmd;
	connectCmd.make("vetc", "vetc@123", 460, 1000, 0, 0);
	connectCmd.serialize(cmd);

	/*AutoSeededRandomPool prng;

	byte key[AES::DEFAULT_KEYLENGTH];
	prng.GenerateBlock(key, sizeof(key));

	byte iv[AES::BLOCKSIZE];
	prng.GenerateBlock(iv, sizeof(iv));*/

	string plain = "";
	for (int i = 0; i < 44; i++) {
		plain.append(1, cmd[i]);
	}

	string cipher, encoded, recovered;

	/*********************************\
	\*********************************/

	// Pretty print key
	encoded.clear();
	StringSource(key, sizeof(key), true,
		new HexEncoder(
			new StringSink(encoded)
		) // HexEncoder
	); // StringSource
	cout << "key: " << encoded << endl;

	// Pretty print iv
	encoded.clear();
	StringSource(iv, sizeof(iv), true,
		new HexEncoder(
			new StringSink(encoded)
		) // HexEncoder
	); // StringSource
	cout << "iv: " << encoded << endl;

	/*********************************\
	\*********************************/

	try
	{
		cout << "plain text: " << endl;
		for (int i = 0; i < 44; i++) {
			cout << std::hex << (char)plain[i] << endl;
		}

		CBC_Mode< AES >::Encryption e;
		e.SetKeyWithIV(key, sizeof(key), iv);

		// The StreamTransformationFilter removes
		//  padding as required.
		StringSource s(plain, true, new StreamTransformationFilter(e, new StringSink(cipher))); // StringSource

		cout << "cipher: " << cipher.length() << endl;
		for (int i = 0; i < cipher.length(); i++) {
			printf("%2x ", (unsigned char)cipher[i]);
		}

#if 0
		StreamTransformationFilter filter(e);
		filter.Put((const byte*)plain.data(), plain.size());
		filter.MessageEnd();

		const size_t ret = filter.MaxRetrievable();
		cipher.resize(ret);
		filter.Get((byte*)cipher.data(), cipher.size());
#endif
	}
	catch (const CryptoPP::Exception& e)
	{
		cerr << e.what() << endl;
		exit(1);
	}

	/*********************************\
	\*********************************/

	// Pretty print
	encoded.clear();
	StringSource(cipher, true,
		new HexEncoder(
			new StringSink(encoded)
		) // HexEncoder
	); // StringSource
	cout << "cipher text: " << encoded << endl;

	/*********************************\
	\*********************************/

	try
	{
		CBC_Mode< AES >::Decryption d;
		d.SetKeyWithIV(key, sizeof(key), iv);

		// The StreamTransformationFilter removes
		//  padding as required.
		StringSource s(cipher, true,
			new StreamTransformationFilter(d,
				new StringSink(recovered)
			) // StreamTransformationFilter
		); // StringSource

#if 0
		StreamTransformationFilter filter(d);
		filter.Put((const byte*)cipher.data(), cipher.size());
		filter.MessageEnd();

		const size_t ret = filter.MaxRetrievable();
		recovered.resize(ret);
		filter.Get((byte*)recovered.data(), recovered.size());
#endif

		cout << "recovered text: " << recovered << endl;
	}
	catch (const CryptoPP::Exception& e)
	{
		cerr << e.what() << endl;
		exit(1);
	}
}

void TCOC_API::testSHA1()
{
	//SHA1        sha;                        // SHA-1 class
	//unsigned    message_digest[5];          // Message digest from "sha"
	//const char* msg = "testtest";

	//sha.Reset();
	//sha.Input(msg, strlen(msg));

	//if (!sha.Result(message_digest))
	//{
	//	printf("sha: could not compute message digest\n");
	//}
	//else
	//{
	//	printf("%08X %08X %08X %08X %08X\n",
	//		message_digest[0],
	//		message_digest[1],
	//		message_digest[2],
	//		message_digest[3],
	//		message_digest[4]);
	//}
}
