#include "PostRequestView.h"
#include "LoginController.h"
#include "EatingController.h"
#include "SessionManager.h"

void handle_post(http_request request)
{
	wstring APIPath = request.absolute_uri().to_string();
	wcout << "\nAPI PATH:" << APIPath;
	cout << "\nhandle POST\n";

	if (wcscmp(APIPath.c_str(), L"/SLCGame311/Login") == 0)
	{
		LoginController Controller;
		Controller.Process(request);
	}
	else if ((wcscmp(APIPath.c_str(), L"/SLCGame311/FoodEaten") == 0))
	{
		EatingController eController;
		eController.Process(request);
	}
	else
	{
		request.reply(status_codes::BadRequest, "error 404 API not found");
	}
}