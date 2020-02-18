#pragma once
#include <cpprest/http_listener.h>
#include <cpprest/json.h>

#include "LoginResponse.h"
#include "LoginRequest.h"
#include "LoginModel.h"

using namespace web;
using namespace web::http;

class LoginController
{
public:
	LoginController();
	~LoginController();

	void Process(http_request request);
};

