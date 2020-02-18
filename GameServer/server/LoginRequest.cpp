#include "LoginRequest.h"



LoginRequest::LoginRequest()
{
}


LoginRequest::~LoginRequest()
{
}

bool LoginRequest::ValidateHeaders(http_request request)
{
	bool success = true;

	// NONE required
	if (!request.headers().has(L"X-men"))
	{
		success = false;
	}

	return success;
}

bool LoginRequest::ProcessRequest(http_request request, LoginModel &playerDiedModel)
{
	//RequestBody = json::value::object();
	bool success = true;

	// grab all the headers [UNUSED]
	http_headers requestHeaders = request.headers();

	//parse the body of the request
	json::value requestJSONData = json::value::object();
	request.extract_json().then([&requestJSONData](pplx::task<json::value> task)
	{
		requestJSONData = task.get();
	}).wait();

	if (requestJSONData.has_string_field(U("PlayerName")))
	{
		utility::string_t Name = requestJSONData.at(U("PlayerName")).as_string();
		wstring initials = Name;
		playerDiedModel.Name = Name;
	}
	else
	{
		success = false;
	}

	return success;
}