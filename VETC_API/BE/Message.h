#pragma once
#include <string.h>

#define MAX_PACKET_SIZE 1024

struct Header {
	unsigned int messageLength;
	unsigned int commandID;
	unsigned int requestID;
	unsigned int sessionID;
};

struct Message {
	Header header;
	char body[MAX_PACKET_SIZE];

    void serialize(char * data)
	{
		memcpy(data, this, sizeof(header) + strlen(body));
    }

    void deserialize(char * data) 
	{
        memcpy(this, data, sizeof(header) + strlen(body));
    }
};