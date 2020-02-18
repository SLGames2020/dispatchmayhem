#include "EatingResponse.h"



EatingResponse::EatingResponse()
{
}


EatingResponse::~EatingResponse()
{
}
bool EatingResponse::ProcessResponse(EatingModel model)
{
	bool success = true;

	// set the UserID inside of our Response
	ResponseBody[L"appleValue"] = json::value::number(model.appleValue);
	ResponseBody[L"popValue"] = json::value::number(model.popValue);
	ResponseBody[L"textReply"] = json::value::string(model.textReply);

	return success;
}

void EatingResponse::SendResponse(http_request request)
{
	// send the JSON
	request.reply(status_codes::OK, ResponseBody);
}
