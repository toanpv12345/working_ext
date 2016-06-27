#pragma once
#include <winsock2.h>
#include <Windows.h>
#include <string>
#include <map>
#include "Network.h"
#include "Message.h"
#include "../Encrypt/aes.h"
#include "../Encrypt/sha1.h"

typedef void(__stdcall *TCOCCallback)(char*, int);

class TCOC_API
{
public:
	TCOC_API(const char* serverIP, int serverPort, TCOCCallback callBack);
	~TCOC_API(void);

	void Connect();
	void Shake();

	void testAES();
	void testSHA1();

private:

	void startReceive();
	void static startReceiveWrapper(void * p);
	char* encrypt(char* packet, int packetSize);
	char* decrypt(char* packetEncript, int packetSize);

	void receivePacketWithEncrypt();
	void sendPacketWithEncrypt(char* packet, int packetSize);

private:
	aes_context m_aesContext;
	AES* m_aes;

	Network* m_network;
	TCOCCallback _callBack;

	std::string m_serverIP;
	int m_serverPort;

	char m_account[20];
	char m_pwd[20];
	int m_staion;

	bool m_enableReceive;

	int m_requestID;
	int m_sessionID;
};

