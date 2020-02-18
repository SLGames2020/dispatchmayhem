#include "PlayerDiedRequest.h"

PlayerDiedRequest::PlayerDiedRequest()
{
}

PlayerDiedRequest::~PlayerDiedRequest()
{
}


bool PlayerDiedRequest::ValidateHeaders(http_request request)
{
	bool success = true;

	if (!request.headers().has(L"UserName"))
	{
		success = false;
	}

	return success;
}

bool PlayerDiedRequest::ProcessRequest(http_request request, PlayerDiedModel &playerDiedModel)
{
	//RequestBody = json::value::object();
	bool success = true;

	// grab all the headers
	http_headers requestHeaders = request.headers();

	// parse out the userID
	playerDiedModel.UserName = requestHeaders[L"UserName"];

	return success;
}