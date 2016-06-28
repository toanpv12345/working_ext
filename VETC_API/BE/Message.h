#pragma once
#include <string>

#define MAX_PACKET_SIZE 1024

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

enum CommandLength {
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

enum ConnectStatus {
	CONN_SUCCESS = 0,
	CONN_AUTHEN_ERROR = 1,
	CONN_CONCURENT_ERROR = 2,
	CONN_CONCURRENT_TIME_ERROR = 3,
	CONN_STATION_ERROR = 4,
	CONN_CONCURRENT_MAX_EROR = 5
};

enum ShakeStatus {
	SHAKE_SUCCESS = 0,
	SHAK_TRANSACTION_EXPIRE = 1
};

enum TerminateStatus {
	TERMINATE_SUCCESS = 0,
	TERMINATE_ERROR = 1
};

struct Header {
	int MessageLength;
	int CommandID;
	int RequestID;
	int SessionID;

	void make(int msgLen, int cmd, int reqId, int sesId) {
		MessageLength = msgLen;
		CommandID = cmd;
		RequestID = reqId;
		SessionID = sesId;
	}
};

struct ConnectCmd {
	Header header;
	char UserName[10];
	char PassWord[10];
	int Station;
	int Timeout;

	void make(char* userName, char* pwd, int station, int timeout, int requestID, int sessionID) {
		Station = station;
		Timeout = timeout;

		header.make(CONNECT_LENGTH, CONNECT, requestID, sessionID);

		memset(UserName, 0, sizeof(UserName));
		memset(PassWord, 0, sizeof(PassWord));

		memcpy(UserName, userName, strlen(userName));
		memcpy(PassWord, pwd, strlen(pwd));
	}

	void serialize(char * data) {
		memcpy(data, this, sizeof(ConnectCmd));
	}
};

struct ConnectResp {
	Header header;
	int Status;
};

// Shake command
struct Shake {
	Header header;

	void make(int requestID, int sessionID) {
		header.make(SHAKE_LENGTH, SHAKE, requestID, sessionID);
	}
};

struct ShakeResp {
	Header header;
	int Status;
};

// Terminate command
struct Terminate {
	Header header;

	void make(int requestID, int sessionID) {
		header.make(TERMINATE_LENGTH, TERMINATE, requestID, sessionID);
	}
};

struct TerminateResp {
	Header header;
	int Status;
};
