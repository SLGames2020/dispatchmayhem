#pragma once

using namespace std;

struct LoginModel
{
	// not serialized
	wstring Name;

	// object specific variables:
	int SessionToken;
	wstring ResponseStr;
};

