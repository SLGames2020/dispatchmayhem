#include "EatingRequest.h"



EatingRequest::EatingRequest()
{
}


EatingRequest::~EatingRequest()
{
}

bool EatingRequest::ValidateHeaders(http_request request)
{
	bool success = true;

	if (!(request.headers().has(L"X-Eats")))
	{
		success = false;
	}

	return success;
}

bool EatingRequest::ProcessRequest(http_request request, EatingModel &eatingModel)
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

	if (requestJSONData.has_integer_field(U("appleCount")) && requestJSONData.has_integer_field(U("popCount")))
	{
		json::number cnt = requestJSONData.at(U("appleCount")).as_number();
		eatingModel.appleCount = cnt.to_int32();
		cnt = requestJSONData.at(U("popCount")).as_number();
		eatingModel.popCount = cnt.to_int32();
	}
	else
	{
		success = false;
	}

	return success;
}
