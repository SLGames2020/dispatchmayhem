#pragma once
#include <cpprest/http_listener.h>
#include <cpprest/json.h>

#include "LoginRequest.h"
#include "LoginModel.h"

using namespace web;
using namespace web::http;
using namespace std;

class LoginResponse
{
public:
	LoginResponse();
	~LoginResponse();

	bool ProcessResponse(LoginModel model);
	void SendResponse(http_request request);

	json::value ResponseBody;
};

