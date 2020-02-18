#include "LoginController.h"
#include "SessionManager.h"

LoginController::LoginController()
{

}

LoginController::~LoginController()
{

}


void LoginController::Process(http_request request)
{
	LoginRequest loginReq;
	LoginModel loginModel;

	// validate headers
	bool success = loginReq.ValidateHeaders(request);
	if (!success)
	{
		request.reply(status_codes::FailedDependency, "Error, Missing or Incorrect Header Information");
	}

	// parse request to populate Model Data
	success = loginReq.ProcessRequest(request, loginModel);
	if (!success)
	{
		request.reply(status_codes::BadRequest, "Error, Unable to Process the Request");
	}

	// Complete Any Logic Here
	bool newUser = !SessionManager::GetInstance()->SessionExists(loginModel.Name);

	loginModel.ResponseStr = loginModel.Name;
	loginModel.SessionToken = -1;

	if (newUser == false)
	{
		loginModel.ResponseStr.append(L" Welcome back");
		loginModel.SessionToken = SessionManager::GetInstance()->GetSession(loginModel.Name);
	}
	else
	{
		loginModel.SessionToken = SessionManager::GetInstance()->CreateSession(loginModel.Name);
		loginModel.ResponseStr.append(L" Welcome to the server. Since this is your first time, please see your authentication token attached.");
	}

	// Process and send the response
	LoginResponse Response;
	success = Response.ProcessResponse(loginModel);
	if (!success)
	{
		request.reply(status_codes::BadRequest, "Error, Unable to Process Response Data");
	}

	Response.SendResponse(request);
}