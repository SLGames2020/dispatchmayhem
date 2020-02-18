#include "PlayerDiedResponse.h"



PlayerDiedResponse::PlayerDiedResponse()
{
}

PlayerDiedResponse::~PlayerDiedResponse()
{
}

bool PlayerDiedResponse::ProcessResponse(PlayerDiedModel model)
{
	bool success = true;
	// set the UserID inside of our Response
	ResponseBody[L"UserName"] = json::value::string(model.UserName);

	return success;
}

void PlayerDiedResponse::SendResponse(http_request request)
{
	// send the JSON
	request.reply(status_codes::OK, ResponseBody);
}
