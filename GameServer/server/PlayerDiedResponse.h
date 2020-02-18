#pragma once
#include <cpprest/http_listener.h>
#include <cpprest/json.h>

#include "PlayerDiedRequest.h"
#include "PlayerDiedModel.h"

using namespace web;
using namespace web::http;
using namespace std;

class PlayerDiedResponse
{
public:
	PlayerDiedResponse();
	~PlayerDiedResponse();

	bool ProcessResponse(PlayerDiedModel model);
	void SendResponse(http_request request);

	json::value ResponseBody;
};

