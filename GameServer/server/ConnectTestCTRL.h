#pragma once
#include <cpprest/http_listener.h>
#include <cpprest/json.h>

#include "GetConnectTestResponse.h"
#include "GetConnectTestRequest.h"
#include "ConnectTestModel.h"

using namespace web;
using namespace web::http;

class ConnectTestCTRL
{
public:
	enum ctrlType
	{
		isGET,
		isPOST
	};	
	
	ConnectTestCTRL();
	~ConnectTestCTRL();

	void Process(http_request request, ctrlType ctyp);
};

