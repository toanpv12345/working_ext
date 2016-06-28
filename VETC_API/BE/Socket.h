#pragma once
#include <winsock2.h>
#include <Windows.h>
#include <ws2tcpip.h>
#include <stdio.h> 
#include "Message.h"

#define DEFAULT_BUFLEN 1024

#pragma comment (lib, "Ws2_32.lib")
#pragma comment (lib, "Mswsock.lib")
#pragma comment (lib, "AdvApi32.lib")

class VETCSocket
{
public:
    VETCSocket(const char * serverIP, const char * serverPort);
    ~VETCSocket(void);

	bool connectBE();
	bool disconnectBE();

	int receivePackets(char *);
	int sendPackets(char *packet, int packetSize);

private:
	SOCKET m_socket;
	const char* m_serverIP;
	const char* m_serverPort;
};

