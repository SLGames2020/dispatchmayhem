#include "GetConnectTestResponse.h"

GetConnectTestResponse::GetConnectTestResponse()
{
}

GetConnectTestResponse::~GetConnectTestResponse()
{
}

bool GetConnectTestResponse::ProcessResponse(ConnectTestModel model)
{
	bool success = true;
	
	ResponseBody[L"UserName"] = json::value::string(model.userName);		// set the UserID inside of our Response
	ResponseBody[L"TestToken"] = json::value::string(model.testToken);

	return success;
}

void GetConnectTestResponse::SendResponse(http_request request)
{
	// send the JSON
	request.reply(status_codes::OK, ResponseBody);
}

