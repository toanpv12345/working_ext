#include "Network.h"

Network::Network(const char * serverIP, const char * serverPort)
{
	m_socket = INVALID_SOCKET;
	m_serverIP = serverIP;
	m_serverPort = serverPort;
}

Network::~Network(void)
{

}

bool Network::connectBE()
{
	if (m_serverIP == nullptr || m_serverPort == nullptr)
		return false;

	WSADATA wsaData;

	m_socket = INVALID_SOCKET;
	struct addrinfo *result = NULL, *ptr = NULL, hints;

	int res = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (res != 0)
		return false;

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_UNSPEC;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;

	res = getaddrinfo(m_serverIP, m_serverPort, &hints, &result);
	if (res != 0)
	{
		WSACleanup();
		return false;
	}

	for (ptr = result; ptr != NULL; ptr = ptr->ai_next)
	{
		m_socket = socket(ptr->ai_family, ptr->ai_socktype, ptr->ai_protocol);

		if (m_socket == INVALID_SOCKET)
		{
			WSACleanup();
			return false;
		}

		res = connect(m_socket, ptr->ai_addr, (int)ptr->ai_addrlen);
		if (res == SOCKET_ERROR)
		{
			closesocket(m_socket);
			m_socket = INVALID_SOCKET;
		}
	}

	freeaddrinfo(result);
	if (m_socket == INVALID_SOCKET)
	{
		WSACleanup();
		return false;
	}

	u_long iMode = 1;
	res = ioctlsocket(m_socket, FIONBIO, &iMode);
	if (res == SOCKET_ERROR)
	{
		closesocket(m_socket);
		WSACleanup();
		return false;
	}

	char value = 1;
	setsockopt(m_socket, IPPROTO_TCP, TCP_NODELAY, &value, sizeof(value));
	return true;
}

bool Network::disconnectBE()
{
	int res = closesocket(m_socket);
	WSACleanup();
	if (res == 0)
		return true;

	return false;
}

int Network::receivePackets(char * recvbuf)
{
	return recv(m_socket, recvbuf, MAX_PACKET_SIZE, 0); //NetworkServices::receiveMessage(m_socket, recvbuf, MAX_PACKET_SIZE);
}

int Network::sendPackets(char * packet, int packetSize)
{
	return send(m_socket, packet, packetSize, 0);
}
