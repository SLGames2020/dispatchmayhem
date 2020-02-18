#include "GetConnectTestRequest.h"

GetConnectTestRequest::GetConnectTestRequest()
{
}

GetConnectTestRequest::~GetConnectTestRequest()
{
}


bool GetConnectTestRequest::ValidateHeaders(http_request request)
{
	bool success = true;

	if (!request.headers().has(L"UserName"))
	{
		success = false;
	}

	return success;
}

bool GetConnectTestRequest::ProcessRequest(http_request request, ConnectTestModel &ctm)
{
	//RequestBody = json::value::object();
	bool success = true;

	http_headers requestHeaders = request.headers();					// grab all the headers

	ctm.userName = requestHeaders[L"UserName"];							// parse out the userID

	return success;
}
