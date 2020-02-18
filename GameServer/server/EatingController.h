#pragma once

#include <cpprest/http_listener.h>
#include <cpprest/json.h>

#include "EatingResponse.h"
#include "EatingRequest.h"
#include "EatingModel.h"

using namespace web;
using namespace web::http;

class EatingController
{
public:
	EatingController() {// totalFood.appleValue = 10;
						}// totalFood.popValue   = 20; }

	~EatingController();

	void Process(http_request request);

private: 
	//static EatingModel totalFood;
};

