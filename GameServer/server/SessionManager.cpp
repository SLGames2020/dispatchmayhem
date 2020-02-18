#include "SessionManager.h"


SessionManager* SessionManager::Instance = nullptr;

SessionManager::SessionManager()
{
}


SessionManager::~SessionManager()
{
}

int SessionManager::CreateSession(wstring key)
{
	return Sessions[key] = g_SessionID;
	g_SessionID++;
}

int SessionManager::GetSession(wstring key) 
{ 
	return Sessions[key]; 
}

bool SessionManager::SessionExists(wstring key)
{
	return Sessions.count(key) != 0;
}
