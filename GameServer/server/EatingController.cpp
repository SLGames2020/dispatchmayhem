#include "EatingController.h"


int appleCount = 0;
int appleValue = 10;
int popCount = 0;
int popValue = 20;

EatingController::~EatingController()
{
}

void EatingController::Process(http_request request)
{
	EatingRequest eatingReq;
	EatingModel eatingModel;

	// validate headers
	bool success = eatingReq.ValidateHeaders(request);
	if (!success)
	{
		request.reply(status_codes::FailedDependency, "Error, Missing or Incorrect Header Information");
	}

	// parse request to populate Model Data
	success = eatingReq.ProcessRequest(request, eatingModel);
	if (!success)
	{
		request.reply(status_codes::BadRequest, "Error, missing items in request");
	}

	if (success)
	{
		appleCount += eatingModel.appleCount;
		popCount += eatingModel.popCount;

		if (appleCount > popCount)
		{
			appleValue -= 1;
			popValue += 1;
		}
		else if (appleCount < popCount)
		{
			appleValue += 1;
			popValue -= 1;
		}

		if (appleValue > popValue)
		{
			eatingModel.textReply.assign(L"Your too skinny. Drink more pop.");
		}
		else if (popValue > appleValue)
		{
			eatingModel.textReply.assign(L"Your getting fat. Eat more apples.");
		}
		else
		{
			eatingModel.textReply.assign(L"You are in good health.");
		}

		eatingModel.appleValue = appleValue;
		eatingModel.popValue = popValue;

		EatingResponse Response;
		success = Response.ProcessResponse(eatingModel);
		if (!success)
		{
			request.reply(status_codes::BadRequest, "Error, Unable to Process Response Data");
		}

		Response.SendResponse(request);
	}
}
