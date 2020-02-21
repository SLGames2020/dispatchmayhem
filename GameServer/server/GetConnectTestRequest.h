#pragma once
#include <cpprest/http_listener.h>
#include <cpprest/json.h>
#include "ConnectTestModel.h"

using namespace web;
using namespace web::http;
using namespace std;

class GetConnectTestRequest
{

public:
	GetConnectTestRequest();
	~GetConnectTestRequest();

	bool ProcessRequest(http_request request, ConnectTestModel &ctm);
	bool ValidateHeaders(http_request request);

	json::value RequestBody;
};