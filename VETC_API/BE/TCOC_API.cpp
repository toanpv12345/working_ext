#include "TCOC_API.h"
#include <process.h>

TCOC_API::TCOC_API(const char* serverIP, int serverPort, const char* account, const char* pwd, int station, unsigned char* encriptKey, TCOCCallback callBack)
{
	m_account = account;
	m_pwd = pwd;
	m_staion = station;
	_callBack = callBack;
	m_enableReceive = false;
	m_requestID = 0;
	m_sessionID = 0;

	m_aes = new AES();
	m_aes->setKey(&m_aesContext, encriptKey, 128);

	printf("m_encriptKey: %s\n", encriptKey);
	printf("m_account: %s\n", m_account.c_str());
	printf("m_pwd: %s\n", m_pwd.c_str());
	printf("m_staion: %d\n", m_staion);

	m_network = new Network(serverIP, std::to_string(serverPort).c_str());
	if (m_network->connectBE())
	{
		m_enableReceive = true;
		_beginthread(&TCOC_API::startReceiveWrapper, 0, this);
	}
}

TCOC_API::~TCOC_API(void)
{

}

void TCOC_API::Connect()
{

}

void TCOC_API::receivePacketWithEncript()
{
	//Packet packet;
	char packet[MAX_PACKET_SIZE] = { 0 };
	int data_length = m_network->receivePackets(packet);

	if (data_length <= 0)
		return;

	char* packetDecript = decript(packet, data_length);

	if (_callBack != nullptr)
		_callBack(packetDecript, data_length);
}

void TCOC_API::sendPacketWithEncript(char * packet, int packetSize)
{
	if (packet == nullptr || packetSize <= 0)
		return;

	char* packetEncript = encript(packet, packetSize);
	m_network->sendPackets(packetEncript, strlen(packetEncript));
}

void TCOC_API::startReceive()
{
	while (m_enableReceive)
	{
		receivePacketWithEncript();
	}
}

void TCOC_API::startReceiveWrapper(void *p)
{
	static_cast<TCOC_API*>(p)->startReceive();
}

char * TCOC_API::encript(char * packet, int packetSize)
{
	unsigned char *buff = new unsigned char[packetSize];
	memcpy_s(buff, packetSize, packet, packetSize);
	m_aes->encrypt(&m_aesContext, buff, buff);
	return (char*)buff;
}

char * TCOC_API::decript(char * packetEncript, int packetSize)
{
	unsigned char *buff = new unsigned char[packetSize];
	memcpy_s(buff, packetSize, packetEncript, packetSize);
	m_aes->decrypt(&m_aesContext, buff, buff);
	return (char*)buff;
}

void TCOC_API::testAES()
{
	int n, i;
	aes_context ctx;
	AES *aes = new AES();
	unsigned char buf[16];
	unsigned char key[32];
	static unsigned char AES_test[16] = {0xF5, 0xBF, 0x8B, 0x37, 0x13, 0x6F, 0x2E, 0x1F,0x6B, 0xEC, 0x6F, 0x57, 0x20, 0x21, 0xE3, 0xBA};

	static unsigned char secret_key[32] =
	{
		0x4E, 0x46, 0xF8, 0xC5, 0x09, 0x2B, 0x29, 0xE2,
		0x9A, 0x97, 0x1A, 0x0C, 0xD1, 0xF6, 0x10, 0xFB,
		0x1F, 0x67, 0x63, 0xDF, 0x80, 0x7A, 0x7E, 0x70,
		0x96, 0x0D, 0x4C, 0xD3, 0x11, 0x8E, 0x60, 0x1A
	};
	/* Keys can be 128 bits, 192 bits, and 256 bits  */
	for (n = 0; n < 3; n++)
	{
		printf("\n-------------------------------------------------\n");
		printf("Test %d: key size = %3d bits\n", n, 128 + n * 64);

		fflush(stdout);

		/* Set the plain-text */
		memcpy(buf, AES_test, 16);

		/* Set the key */
		memcpy(key, secret_key, 16 + n * 8);
		aes->setKey(&ctx, key, 128 + n * 64);

		/* Print out the plain text */
		for (i = 0; i < 16; i++) {
			printf("%2x ", buf[i]);
		}
		printf(" (Plain Text)\n");

		aes->encrypt(&ctx, buf, buf);
		for (i = 0; i < 16; i++) {
			printf("%2x ", buf[i]);
		}
		printf(" (Cipher Text)\n");


		aes->decrypt(&ctx, buf, buf);
		for (i = 0; i < 16; i++) {
			printf("%2x ", buf[i]);
		}
		printf(" (Decrypted Plain Text)\n");
	}
}

void TCOC_API::testSHA1()
{
	SHA1        sha;                        // SHA-1 class
	unsigned    message_digest[5];          // Message digest from "sha"
	const char* msg = "testtest";

	sha.Reset();
	sha.Input(msg, strlen(msg));

	if (!sha.Result(message_digest))
	{
		printf("sha: could not compute message digest\n");
	}
	else
	{
		printf("%08X %08X %08X %08X %08X\n",
			message_digest[0],
			message_digest[1],
			message_digest[2],
			message_digest[3],
			message_digest[4]);
	}
}
