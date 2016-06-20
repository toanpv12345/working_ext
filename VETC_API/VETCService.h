// VETC_API.h

#pragma once
#include "BE\TCOC_API.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace VETC {

	public delegate void VETCServiceEvent(String^ packet);
	public delegate void TCOCDelegate(char* buffer);

	public ref class VETCService
	{
	private:
		TCOC_API* _TCOC_API;

	public:
		VETCService(String^ serverIP, int serverPort, String^ account, String^ pwd, int station, String^ encriptKey);
		~VETCService();

		static event VETCServiceEvent^ ServiceEvent;
		static void TCOCReceiver(char* packet);
	};
}
