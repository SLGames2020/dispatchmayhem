#pragma once
#include <cpprest/http_listener.h>
#include <cpprest/json.h>

#include "PlayerDiedResponse.h"
#include "PlayerDiedRequest.h"
#include "PlayerDiedModel.h"

using namespace web;
using namespace web::http;

class PlayerDiedController
{
public:
	PlayerDiedController();
	~PlayerDiedController();

	void Process(http_request request);
};

