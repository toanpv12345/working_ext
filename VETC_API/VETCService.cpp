// This is the main DLL file.

#include "stdafx.h"

#include "VETCService.h"

namespace VETC {
	VETCService::VETCService(String^ serverIP, int serverPort, String^ account, String^ pwd, int station, String^ encriptKey)
	{
		TCOCDelegate^ callback = gcnew TCOCDelegate(&VETCService::TCOCReceiver);
		IntPtr cbPtr = Marshal::GetFunctionPointerForDelegate(callback);
		//Marshal::GetDelegateForFunctionPointer();

		IntPtr pip = Marshal::StringToHGlobalAnsi(serverIP);
		char *cip = static_cast<char*>(pip.ToPointer());

		IntPtr pkey = Marshal::StringToHGlobalAnsi(encriptKey);
		unsigned char *ckey = static_cast<unsigned char*>(pkey.ToPointer());

		IntPtr pacc = Marshal::StringToHGlobalAnsi(account);
		char *cacc = static_cast<char*>(pacc.ToPointer());

		IntPtr ppwd = Marshal::StringToHGlobalAnsi(pwd);
		char *cpwd = static_cast<char*>(ppwd.ToPointer());

		_TCOC_API = new TCOC_API(cip, serverPort, cacc, cpwd, station, ckey, static_cast<TCOCCallback>(cbPtr.ToPointer()));

		Marshal::FreeHGlobal(ppwd);
		Marshal::FreeHGlobal(pacc);
		Marshal::FreeHGlobal(pkey);
		Marshal::FreeHGlobal(pip);
	}

	VETCService::~VETCService()
	{
		delete _TCOC_API;
	}

	void VETCService::TCOCReceiver(char* packet)
	{
		ServiceEvent(gcnew String(packet));
	}
}