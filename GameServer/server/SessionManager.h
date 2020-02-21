#pragma once
#include <map>
#include <string>

using namespace std;

class SessionManager
{
public:
	static SessionManager* GetInstance()
	{
		if (Instance == nullptr)
		{
			Instance = new SessionManager();
		}

		return Instance;
	}

	bool SessionExists(wstring key);
	int GetSession(wstring key);
	int CreateSession(wstring key);

private:
	SessionManager();
	~SessionManager();

	static SessionManager* Instance;
	map<wstring, int> Sessions;

	int g_SessionID = 8795;
};
