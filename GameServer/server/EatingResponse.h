#pragma once
#include <cpprest/http_listener.h>
#include <cpprest/json.h>

#include "EatingRequest.h"
#include "EatingModel.h"


class EatingResponse
{
public:
	EatingResponse();
	~EatingResponse();

	bool ProcessResponse(EatingModel model);
	void SendResponse(http_request request);

	json::value ResponseBody;
};

