#pragma once
#include <cpprest/http_listener.h>
#include <cpprest/json.h>
#include "PlayerDiedModel.h"

using namespace web;
using namespace web::http;
using namespace std;

class PlayerDiedRequest
{
public:
	PlayerDiedRequest();
	~PlayerDiedRequest();

	bool ProcessRequest(http_request request, PlayerDiedModel &playerDiedModel);
	bool ValidateHeaders(http_request request);

	json::value RequestBody;
};

