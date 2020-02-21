#include "PlayerDiedController.h"


PlayerDiedController::PlayerDiedController()
{
}


PlayerDiedController::~PlayerDiedController()
{
}


void PlayerDiedController::Process(http_request request)
{
	PlayerDiedRequest playerDiedReq;
	PlayerDiedModel playerDiedModel;

	// validate headers
	bool success = playerDiedReq.ValidateHeaders(request);
	if (!success)
	{
		request.reply(status_codes::FailedDependency, "Error, Missing or Incorrect Header Information");
	}
	bool hldsuccess = success;											//track for no errors to send OK message to the console

	// parse request to populate Model Data
	success = playerDiedReq.ProcessRequest(request, playerDiedModel);
	hldsuccess |= success;
	if (!success)
	{
		request.reply(status_codes::BadRequest, "Error, Unable to Process the Request");
	}

	// Complete Any Logic Here


	// Process and send the response
	PlayerDiedResponse Response;
	success = Response.ProcessResponse(playerDiedModel);
	hldsuccess |= success;
	if (!success)
	{
		request.reply(status_codes::BadRequest, "Error, Unable to Process Response Data");
	}

	Response.SendResponse(request);

//	if (hldsuccess)
//	{
//		std::cout << playerDiedModel << " died well" << endl;
//	}
}