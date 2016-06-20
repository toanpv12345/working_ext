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

enum CommandID {
	CONNECT = 0x00,
	CONNECT_RESP = 0x01,
	SHAKE = 0x02,
	SHAKE_RESP = 0x03,
	CHECKIN = 0x04,
	CHECKIN_RESP = 0x05,
	COMMIT = 0x06,
	COMMIT_RESP = 0x07,
	ROLLBACK = 0x08,
	ROLLBACK_RESP = 0x09,
	TERMINATE = 0x0A,
	TERMINATE_RESP = 0x0B,
	CHARGE = 0x0C,
	CHARGE_RESP = 0x0D
};

enum LengthCommand {
	CONNECT_LENGTH = 44,
	CONNECT_RESP_LENGTH = 20,
	SHAKE_LENGTH = 16,
	SHAKE_RESP_LENGTH = 20,
	CHECKIN_LENGTH = 78,
	CHECKIN_RESP_LENGTH = 82,
	COMMIT_LENGTH = 74,
	COMMIT_RESP_LENGTH = 20,
	ROLLBACK_LENGTH = 70,
	ROLLBACK_RESP_LENGTH = 20,
	TERMINATE_LENGTH = 16,
	TERMINATE_RESP_LENGTH = 20,
	CHARGE_LENGTH = 48,
	CHARGE_RESP_LENGTH = 74
};

class TCOC_API
{
public:
	TCOC_API(const char* serverIP, int serverPort, const char* account, const char* pwd, int station, unsigned char* encriptKey, TCOCCallback callBack);
	~TCOC_API(void);

	void Connect();
	void Shake();

	void testAES();
	void testSHA1();

private:

	void startReceive();
	void static startReceiveWrapper(void * p);
	char* encript(char* packet, int packetSize);
	char* decript(char* packetEncript, int packetSize);

	void receivePacketWithEncript();
	void sendPacketWithEncript(char* packet, int packetSize);

private:
	aes_context m_aesContext;
	AES* m_aes;

	Network* m_network;
	TCOCCallback _callBack;

	std::string m_serverIP;
	int m_serverPort;

	std::string m_account;
	std::string m_pwd;
	int m_staion;

	bool m_enableReceive;

	int m_requestID;
	int m_sessionID;
};

