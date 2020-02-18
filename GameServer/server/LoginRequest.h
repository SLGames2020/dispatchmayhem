#pragma once
#include <cpprest/http_listener.h>
#include <cpprest/json.h>
#include "LoginModel.h"

using namespace web;
using namespace web::http;
using namespace std;

class LoginRequest
{
public:
	LoginRequest();
	~LoginRequest();

	bool ProcessRequest(http_request request, LoginModel &playerDiedModel);
	bool ValidateHeaders(http_request request);

	json::value RequestBody;
};

