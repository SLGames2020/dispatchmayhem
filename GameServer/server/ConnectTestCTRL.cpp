#include "ConnectTestCTRL.h"

ConnectTestCTRL::ConnectTestCTRL()
{
}


ConnectTestCTRL::~ConnectTestCTRL()
{
}


void ConnectTestCTRL::Process(http_request request, ctrlType ctyp)
{
	GetConnectTestRequest getConnectTestReq;
	GetConnectTestResponse Response;
	ConnectTestModel  connectTestModel;

	// validate headers
	//bool success = getConnectTestReq.ValidateHeaders(request);
	if (!getConnectTestReq.ValidateHeaders(request))
	{
		request.reply(status_codes::FailedDependency, "Error, Missing or Incorrect Header Information");
		cout << "Invalid Header" << endl;
	}
	//bool hldsuccess = success;											//track for no errors to send OK message to the console
	//success = getConnectTestReq.ProcessRequest(request, connectTestModel);	// parse request to populate Model Data
	//hldsuccess |= success;
	else if (!getConnectTestReq.ProcessRequest(request, connectTestModel))
	{
		request.reply(status_codes::BadRequest, "Error, Unable to Process the Request");
		cout << "Unable to Process Request" << endl;
	}
	else																						// Complete Any Logic Here	
	{
		if (connectTestModel.userName == L"JVT")
		{
			connectTestModel.testToken.assign(L"Token Granted");
		}
		else
		{
			connectTestModel.testToken.assign(L"Session denied");
		}
		wcout << connectTestModel.userName << " Test: " << connectTestModel.testToken << endl;

		// Process and send the response
		//GetConnectTestResponse Response;
		//success = Response.ProcessResponse(connectTestModel);
		//hldsuccess |= success;
		//if (!success)
		if (!Response.ProcessResponse(connectTestModel))
		{
			request.reply(status_codes::BadRequest, "Error, Unable to Process Response Data");
			cout << "Unable to Process Response" << endl;
		}
	}
	Response.SendResponse(request);

}
