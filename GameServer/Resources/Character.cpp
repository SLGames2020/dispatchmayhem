#include "Character.h"
#include <iostream>
#include <stdlib.h>     /* srand, rand */
#include <time.h>       /* time */

using namespace std; 
using namespace web;

Character::Character(std::string charName)
{
	// start off with points to distribute for stats
	const int STAT_POOL = 400;
	int remainingPoints = STAT_POOL;

	srand(time(0));
	strcpy_s(name, charName.c_str() );

	HP = 0;
	SP = 0;

	INT = 0;
	ATT = 0;
	DEF = 0;
	AGI = 0;
	LCK = 0;

	while(remainingPoints > 0)
	{
		int allocation = 0;
		allocation = rand() % 10 + 1;
		HP += allocation;
		remainingPoints -= allocation;

		allocation = rand() % 5 + 1;
		SP += allocation;
		remainingPoints -= allocation;

		allocation = rand() % 5 + 1;
		ATT += allocation;
		remainingPoints -= allocation;

		allocation = rand() % 5 + 1;
		DEF += allocation;
		remainingPoints -= allocation;

		allocation = rand() % 5 + 1;
		AGI += allocation;
		remainingPoints -= allocation;

		allocation = rand() % 5 + 1;
		INT += allocation;
		remainingPoints -= allocation;

		allocation = rand() % 5 + 1;
		LCK += allocation;
		remainingPoints -= allocation;
	}

	currHP = HP;
	currSP = SP;
}


Character::~Character()
{
}

void Character::PrintStats()
{
	cout << "Name: " << name << endl;
	cout << "===========================" << endl;
	cout << "HP: " << HP << "\t\t";
	cout << "SP: " << SP << endl;
	cout << "ATT: " << ATT << "\t\t";
	cout << "DEF: " << DEF << endl;
	cout << "AGI: " << AGI << "\t\t";
	cout << "INT: " << INT << endl;
	cout << "===========================" << endl;

}

// JD ADDED A NEW FUNCTION TO CONVERT OUR CHARACTER INTO A JSON OBJECT
json::value Character::asJSON()
{
	json::value JSONObj = json::value::object();
	JSONObj[L"HP"] = HP;
	JSONObj[L"SP"] = SP;
	return JSONObj;
}