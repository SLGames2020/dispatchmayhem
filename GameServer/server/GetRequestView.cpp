#include "GetRequestView.h"
#include "PlayerDiedController.h"
#include "ConnectTestCTRL.h"

void handle_get(http_request request)
{
	wstring APIPath = request.absolute_uri().to_string();

	wcout << "\nAPI PATH:" << APIPath;
	cout << " :: handle GET\n";

	if (wcscmp(APIPath.c_str(), L"/SLCGame311/PlayerDied") == 0)
	{
		PlayerDiedController Controller;
		Controller.Process(request);
	}
	else if (wcscmp(APIPath.c_str(), L"/DispatchMayhem/GetConnectTest") == 0)
	{
		ConnectTestCTRL testCtrl;
		testCtrl.Process(request, ConnectTestCTRL::isGET);
	}
	else
	{
		request.reply(status_codes::BadRequest, "Requested API not found.");
	}
}