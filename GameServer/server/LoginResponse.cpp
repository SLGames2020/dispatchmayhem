#include "LoginResponse.h"

LoginResponse::LoginResponse()
{
}

LoginResponse::~LoginResponse()
{
}

bool LoginResponse::ProcessResponse(LoginModel model)
{
	bool success = true;

	// set the UserID inside of our Response
	ResponseBody[L"ResponseStr"] = json::value::string(model.ResponseStr);
	ResponseBody[L"SessionToken"] = json::value::number(model.SessionToken);

	return success;
}

void LoginResponse::SendResponse(http_request request)
{
	// send the JSON
	request.reply(status_codes::OK, ResponseBody);
}
