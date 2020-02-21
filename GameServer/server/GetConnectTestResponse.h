#pragma once
#include <cpprest/http_listener.h>
#include <cpprest/json.h>

#include "GetConnectTestRequest.h"
#include "ConnectTestModel.h"

using namespace web;
using namespace web::http;
using namespace std;

class GetConnectTestResponse
{
public:
	GetConnectTestResponse();
	~GetConnectTestResponse();

	bool ProcessResponse(ConnectTestModel model);
	void SendResponse(http_request request);

	json::value ResponseBody;
};

