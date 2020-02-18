#pragma once

#include <cpprest/http_listener.h>
#include <cpprest/json.h>
#include "EatingModel.h"

using namespace web;
using namespace web::http;
using namespace std;

class EatingRequest
{
public:
	EatingRequest();
	~EatingRequest();

	bool ProcessRequest(http_request request, EatingModel &eatingModel);
	bool ValidateHeaders(http_request request);

	json::value RequestBody;
};

