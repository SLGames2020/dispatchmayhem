#include <string>
//JD ADDED THIS HEADER FOR JSON DATA
#include <cpprest/json.h>

using namespace web;
#pragma once
class Character
{
public:

	Character(std::string charName);
	~Character();

	//JD ADDED A JSON EXPORTING FUNCTION
	web::json::value asJSON();

	void PrintStats();

private:
	char name[200];

	int HP;
	int SP;
	int currHP;
	int currSP;
	int ATT;
	int DEF;
	int INT;
	int AGI;
	int LCK;
};

